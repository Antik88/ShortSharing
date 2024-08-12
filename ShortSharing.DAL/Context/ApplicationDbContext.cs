using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Entities;
using ShortSharing.DAL.Interceptors;

namespace ShortSharing.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditableEntitiesInterceptor _auditInterceptor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            AuditableEntitiesInterceptor auditInterceptor) : base(options)
        {
            _auditInterceptor = auditInterceptor;

            if (Database.IsNpgsql())
            {
                Database.Migrate();
            }
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ThingEntity> Things { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
    }
}
