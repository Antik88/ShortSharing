using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Service.DAL.Constants;
using Rent.Service.DAL.Context;
using Rent.Service.DAL.Repositories.Implementations;
using Rent.Service.DAL.Repositories.Interfaces;

namespace Rent.Service.DAL.DI;

public static class ServicesConfiguration
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(DataAccessConstants.DbConnection));
        });

        services.AddScoped(typeof(IRentRepository), typeof(RentRepository));
    }
}
