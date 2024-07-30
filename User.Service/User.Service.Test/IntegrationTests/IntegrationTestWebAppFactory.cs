using Xunit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using User.Service.DLL.Context;
using User.Service.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.Service.Test.IntegrationTests;

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

            Seed.InitializeTestDatabase(dbContext);

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
