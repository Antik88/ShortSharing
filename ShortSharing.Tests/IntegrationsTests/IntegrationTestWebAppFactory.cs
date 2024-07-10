using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShortSharing.API;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.DI;
using ShortSharing.Tests.Constants;

namespace ShortSharing.Tests.IntegrationsTests;


public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var configuration = new ConfigurationBuilder()
                   .AddJsonFile(TestingConstants.AppSettings)
                   .Build();

            services.AddDataAccessDependencies(configuration);
        });
    }
}
