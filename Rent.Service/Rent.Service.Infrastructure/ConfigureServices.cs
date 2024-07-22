using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Service.Domain.Repository;
using Rent.Service.Infrastructure.Consts;
using Rent.Service.Infrastructure.Data;
using Rent.Service.Infrastructure.Repository;

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

        services.AddTransient<IRentRepository, RentRepository>();
        return services; 
    }
}
