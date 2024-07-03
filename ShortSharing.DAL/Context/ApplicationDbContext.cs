using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (Database.IsNpgsql())
            {
                Database.Migrate();
            }
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RentEntity> Rents { get; set; }
        public DbSet<ThingEntity> Things { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
