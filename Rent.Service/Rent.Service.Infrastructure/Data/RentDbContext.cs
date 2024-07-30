using Microsoft.EntityFrameworkCore;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Infrastructure.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions<RentDbContext> options) : base(options)
    { }

    public DbSet<RentEntity> Rents { get; set; }
}
