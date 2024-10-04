using Microsoft.EntityFrameworkCore;
using User.Service.DLL.Entities;

namespace User.Service.DLL.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        if (Database.IsSqlServer())
        {
            Database.Migrate();
        }
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.AuthId)
            .IsUnique(true);
    }
}
