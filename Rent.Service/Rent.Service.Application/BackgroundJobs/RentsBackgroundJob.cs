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

    private Task ProcessRentsAsync(List<RentEntity> rents, CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();

        foreach (var rent in rents)
        {
            tasks.Add(ProcessSingleRentAsync(rent, cancellationToken));
        }

        return Task.WhenAll(tasks);
    }

    private async Task ProcessSingleRentAsync(RentEntity rent, CancellationToken cancellationToken)
    {
        bool statusChanged = await rentStatusChanger.IsStatusChanged(rent);

        if (statusChanged)
        {
            await rentNotificationPublisher.SendRentMessage(rent,
                MessageType.RentStatusChange, cancellationToken);
        }
    }
}
