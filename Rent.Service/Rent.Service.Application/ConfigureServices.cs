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
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddQuartz(options =>
        {
            options.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();
        });

        services.AddQuartzHostedService();

        services.ConfigureOptions<RentsBackgroundJobSetUp>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        return services;
    }
}