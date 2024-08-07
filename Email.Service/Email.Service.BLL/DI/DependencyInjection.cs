using Email.Service.BLL.Interfaces;
using Email.Service.BLL.Scheduling;
using Email.Service.BLL.Service;
using Email.Service.DAL.DI;
using Email.Service.Interfaces;
using Email.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;

namespace Email.Service.BLL.DI;

public static class DependencyInjection
{
    public static void AddBusinessLogicDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IEmailSender, EmailService>();
        services.AddTransient<IRentService, RentService>();

        services.AddQuartz(options =>
        {
            options.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

            var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));

            options
                .AddJob<LoggingBackgroundJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInHours(1).RepeatForever()));
        });

        services.AddQuartzHostedService();

        services.AddDataAccessDependencies(configuration);
    }
}
