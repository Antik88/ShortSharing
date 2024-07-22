using Microsoft.EntityFrameworkCore;
using Rent.Service.DAL.Entites;

namespace Rent.Service.DAL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public DbSet<RentEntity> Rents { get; set; }
}
