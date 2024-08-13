using Quartz;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Domain.Entity;
using Rent.Service.Infrastructure;
using SharingMessages;

namespace Rent.Service.Application.BackgroundJobs;

[DisallowConcurrentExecution]
public class RentsBackgroundJob(IRentQueryRepository rentQuery,
    IRentStatusChanger rentStatusChanger,
    IRentNotification rentNotificationPublisher,
    IExternalServiceRequests<ICatalogServiceHttpClient> catalogServiceRequests,
    IExternalServiceRequests<IUserServiceHttpClient> userServiceRequests) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var cancellationToken = context.CancellationToken;

        var notCompletedRents = await rentQuery.GetNotExpiredRents();

        await ProcessRentsAsync(notCompletedRents, cancellationToken);
    }

    private async Task ProcessRentsAsync(List<RentEntity> rents, CancellationToken cancellationToken)
    {
        foreach (var rent in rents)
        {
            bool statusChanged = await rentStatusChanger.IsStatusChanged(rent);

            if (!statusChanged)
                continue;

            var thingModel = await catalogServiceRequests.GetFromServiceById<ThingModel>(
                rent.ThingId, cancellationToken);

            var tenantModel = await userServiceRequests.GetFromServiceById<UserModel>(
                rent.TenantId, cancellationToken);

            var ownerModel = await userServiceRequests.GetFromServiceById<UserModel>(
                thingModel.OwnerId, cancellationToken);

            await rentNotificationPublisher.SendRentMessage(new RentRecord(
                rent.Id,
                thingModel,
                ownerModel,
                tenantModel,
                rent.StartRentDate,
                rent.EndRentDate));
        }
    }
}
