using Email.Service.DAL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Service.DAL.DI;

public static class DependencyInjection
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbService>();
    }
}
