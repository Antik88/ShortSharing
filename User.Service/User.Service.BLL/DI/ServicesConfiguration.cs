using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Service.BLL.Mappers;
using User.Service.BLL.Service.Implementation;
using User.Service.BLL.Service.Interfaces;
using User.Service.DLL.DI;

namespace User.Service.BLL.DI;

public static class ServicesConfiguration
{
    public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessDependencies(configuration);

        services.AddAutoMapper(typeof(BLLProfile).Assembly);

        services.AddScoped<IUserService, UserService>();
    }
}

