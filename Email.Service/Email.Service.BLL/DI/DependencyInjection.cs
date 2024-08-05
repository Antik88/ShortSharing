using Email.Service.BLL.Interfaces;
using Email.Service.BLL.Service;
using Email.Service.DAL.DI;
using Email.Service.Interfaces;
using Email.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Service.BLL.DI;

public static class DependencyInjection
{
    public static void AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailSender, EmailService>();
        services.AddTransient<IRentService, RentService>();

        services.AddDataAccessDependencies(configuration);
    }
}
