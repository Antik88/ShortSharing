using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Service.BLL.Services.Implementations;
using Rent.Service.BLL.Services.Interfaces;
using Rent.Service.DAL.DI;

namespace Rent.Service.BLL.DI;

public static class ServicesConfiguration
{
    public static void AddBLLDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessDependencies(configuration);

        services.AddHttpClient();

        services.AddScoped<IRentService, RentService>();
    }
}
