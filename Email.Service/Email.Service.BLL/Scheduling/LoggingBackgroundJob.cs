using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Email.Service.BLL.Scheduling;

public class LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger,
    IRentService rentService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{UtcNow}", DateTime.UtcNow);

        var rents = await rentService.GetByRentStatus(RentStatus.Pending);

        await ChangeStatus(rents);

        await Task.CompletedTask;
    }

    private Task ChangeStatus(List<RentEntity> rents)
    {
        rents.ForEach(rent => rent.Status = RentStatus.Active);

        return Task.CompletedTask;
    }
}
