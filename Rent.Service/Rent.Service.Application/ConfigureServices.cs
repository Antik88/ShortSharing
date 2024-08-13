using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Rent.Service.Application.BackgroundJobs;
using Rent.Service.Application.Common.Behaviors;
using System.Reflection;

namespace Rent.Service.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        service.AddQuartz(options =>
        {
            options.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();
        });

        service.AddQuartzHostedService();

        service.ConfigureOptions<RentsBackgroundJobSetUp>();

        service.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        return service;
    }
}
