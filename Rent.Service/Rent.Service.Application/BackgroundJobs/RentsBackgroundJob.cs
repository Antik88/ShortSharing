using Quartz;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Domain.Entity;
using SharingMessages;

namespace Rent.Service.Application.BackgroundJobs;

[DisallowConcurrentExecution]
public class RentsBackgroundJob(IRentQueryRepository rentQuery,
        IRentStatusChanger rentStatusChanger,
        IRentNotification rentNotificationPublisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var notCompletedRents = await rentQuery.GetNotExpiredRents();

        await ProcessRentsAsync(notCompletedRents, context.CancellationToken);
    }

    private async Task ProcessRentsAsync(List<RentEntity> rents, CancellationToken cancellationToken)
    {
        foreach (var rent in rents)
        {
            await ProcessSingleRentAsync(rent, cancellationToken);
        }
    }

    private async Task ProcessSingleRentAsync(RentEntity rent, CancellationToken cancellationToken)
    {
        bool statusChanged = await rentStatusChanger.IsStatusChanged(rent);

        if (statusChanged)
        {
            await rentNotificationPublisher.SendRentMessage(rent, MessageType.RentStatusChange, cancellationToken);
        }
    }
}
