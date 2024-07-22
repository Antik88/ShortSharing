using Microsoft.EntityFrameworkCore;
using Rent.Service.Domain.Entity;
using Rent.Service.Domain.Repository;
using Rent.Service.Infrastructure.Data;

namespace Rent.Service.Infrastructure.Repository;

public class RentRepository(RentDbContext context) : IRentRepository
{
    public async Task<RentEntity> CreateAsync(RentEntity rentEntity)
    {
        await context.Rents.AddAsync(rentEntity);

        await context.SaveChangesAsync();

        return rentEntity;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await context.Rents
           .Where(model => model.Id == id)
           .ExecuteDeleteAsync();
    }

    public async Task<List<RentEntity>> GetAllRentsAsync()
    {
        return await context.Rents.ToListAsync();
    }

    public async Task<RentEntity> GetByIdAsync(Guid id)
    {
        return await context.Rents.AsNoTracking()
            .FirstOrDefaultAsync(model => model.Id == id);
    }

    public async Task<int> UpdateAsync(Guid id, RentEntity rentEntity)
    {
       return await context.Rents
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.StartRentDate, rentEntity.StartRentDate)
                .SetProperty(m => m.EndRentDate, rentEntity.EndRentDate)
            );
    }
}
