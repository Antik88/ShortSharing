using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Rent.Service.Application;
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
        services.AddDbContext<RentDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(
                DatabaseConstants.DbConnection));
        });

        var retryPolicy = HttpPolicyExtensions 
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


        services.AddHttpClient<IUserServiceHttpClient, UserServiceHttpClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetConnectionString("UserBaseUrl"));
        })
        .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ICatalogServiceHttpClient, CatalogServiceHttpClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetConnectionString("CatalogueBaseUrl"));
        })
        .AddPolicyHandler(retryPolicy);

        services.AddScoped<IRentAvailabilityRepository, RentRepository>();
        services.AddScoped<IRentExtensionRepository, RentRepository>();
        services.AddScoped<IRentManagementRepository, RentRepository>();
        services.AddScoped<IRentQueryRepository, RentRepository>();
        services.AddScoped<IRentStatusChanger, RentRepository>();

        services.AddScoped<IRentNotification, RentNotificationPublisher>();

        services.AddScoped(typeof(IExternalServiceRequests<>), typeof(ExternalServiceRequests<>));

        return services;
    }
}
