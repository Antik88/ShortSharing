using Microsoft.EntityFrameworkCore;
using User.Service.DLL.Entities;

namespace User.Service.DLL.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
}
