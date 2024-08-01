using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Infrastructure.Consts;
using Rent.Service.Infrastructure.Data;
using Rent.Service.Infrastructure.Repository;
using Rent.Service.Infrastructure.Service;

namespace Rent.Service.Infrastructure;

public static class ConfigureServices  
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RentDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString(
                DatabaseConstants.DbConnection));
        });

        services.AddTransient<IRentAvailabilityRepository, RentRepository>();
        services.AddTransient<IRentExtensionRepository, RentRepository>();
        services.AddTransient<IRentManagementRepository, RentRepository>();
        services.AddTransient<IRentQueryRepository, RentRepository>();

        services.AddScoped<IRentNotification, RentNotificationPublisher>();
        services.AddScoped<IExternalServiceRequests, ExternalServiceRequests>();

        return services; 
    }
}
