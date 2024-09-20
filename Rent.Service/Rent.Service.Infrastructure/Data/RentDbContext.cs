using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Infrastructure.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions<RentDbContext> options) : base(options)
    {
        var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if(databaseCreator != null)
        {
            if (!databaseCreator.CanConnect()) databaseCreator.Create();
            if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
        }
    }

    public DbSet<RentEntity> Rents { get; set; }
}
