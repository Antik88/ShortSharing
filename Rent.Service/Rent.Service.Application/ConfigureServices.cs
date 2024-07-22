
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rent.Service.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        return service;
    }
}
