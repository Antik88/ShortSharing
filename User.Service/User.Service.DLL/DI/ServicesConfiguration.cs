using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Service.DLL.Context;
using User.Service.DLL.Repositories.Implementation;
using User.Service.DLL.Repositories.Interfaces;

namespace User.Service.DLL.DI;

public static class ServicesConfiguration
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
