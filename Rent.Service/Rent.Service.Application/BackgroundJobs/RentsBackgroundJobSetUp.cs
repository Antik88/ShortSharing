using Microsoft.Extensions.Options;
using Quartz;

namespace Rent.Service.Application.BackgroundJobs;

public class RentsBackgroundJobSetUp : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(RentsBackgroundJob));

        options
            .AddJob<RentsBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInHours(5).RepeatForever()));
    }
}
