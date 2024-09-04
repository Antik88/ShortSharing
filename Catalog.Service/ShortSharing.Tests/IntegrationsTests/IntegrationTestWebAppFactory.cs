using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShortSharing.API;
using ShortSharing.DAL.Context;
using System.Net.Http.Headers;
using Xunit;

namespace ShortSharing.Tests.IntegrationsTests
{
    public class IntegrationTestWebAppFactory : IAsyncLifetime
    {
        private const string _connectionString = "test";
        private WebApplicationFactory<Program> _factory;
        public HttpClient Client { get; private set; }

        public IntegrationTestWebAppFactory()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(_connectionString);
                    });

                    services.AddMassTransitTestHarness();
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
                });
            });

            Client = _factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();

                Seeding.InitializeTestDatabase(dbContext);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DisposeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

                await dbContext.Database.EnsureDeletedAsync();
                Client.Dispose();

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
