using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShortSharing.DAL.Constants;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Interceptors;

namespace ShortSharing.DAL.Common
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../ShortSharing.API/ShortSharing.API/"))
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
               .AddSingleton<AuditableEntitiesInterceptor>()
               .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseNpgsql(configuration.GetConnectionString(DataAccessConstants.DbConnection));

            var auditInterceptor = serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>();

            var context = new ApplicationDbContext(options.Options, auditInterceptor);
            context.Database.Migrate();

            return context;
        }
    }
}
