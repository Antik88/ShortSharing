using Microsoft.EntityFrameworkCore;
using Rent.Service.Domain.Entity;
using Rent.Service.Application.Abstractions;
using Rent.Service.Infrastructure.Data;

namespace Rent.Service.Infrastructure.Repository;

public class RentRepository(RentDbContext context) : IRentManagementRepository,
    IRentQueryRepository, 
    IRentAvailabilityRepository, 
    IRentExtensionRepository
{
    public async Task<RentEntity> CreateAsync(RentEntity rentEntity)
    {
        await context.Rents.AddAsync(rentEntity);

        await context.SaveChangesAsync();

        return rentEntity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await context.Rents
           .Where(model => model.Id == id)
           .ExecuteDeleteAsync();
    }

    public Task<List<RentEntity>> GetAllRentsAsync()
    {
        return  context.Rents.ToListAsync();
    }

    public async Task<RentEntity> GetByIdAsync(Guid id)
    {
        return await context.Rents.AsNoTracking()
            .FirstOrDefaultAsync(model => model.Id == id);
    }

    public async Task<List<RentEntity>> GetByUserId(Guid userId)
    {
        return await context.Rents.AsNoTracking()
            .Where(model => model.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<RentEntity>> GetRentsForThingAsync(Guid thingId)
    {
        return await context.Rents
            .AsNoTracking()
            .Where(rent => rent.ThingId == thingId)
            .ToListAsync();
    }

    public async Task<RentEntity> UpdateAsync(Guid id, DateTime startRentDate, DateTime endRentDate)
    {
        await context.Rents
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.StartRentDate, startRentDate)
                .SetProperty(m => m.EndRentDate, endRentDate)
            );

        var updatedEntity = await context.Rents
            .Where(model => model.Id == id)
            .FirstOrDefaultAsync();

        return updatedEntity;
    }

    public async Task<bool> IsAvailableAsync(Guid thingId, DateTime startRentDate, DateTime endRentDate)
    {
        var existingRents = await GetRentsForThingAsync(thingId);

        return existingRents.All(rent =>
            rent.EndRentDate <= startRentDate || rent.StartRentDate >= endRentDate);
    }

    public async Task<RentEntity> ExtendRentAsync(Guid rentId, DateTime newEndRentDate)
    {
        var rentEntity = await context.Rents.FindAsync(rentId);

        if (rentEntity is null)
            throw new KeyNotFoundException("Rent not found.");

        rentEntity.EndRentDate = newEndRentDate;
        context.Rents.Update(rentEntity);
        await context.SaveChangesAsync();
        return rentEntity;
    }

    public async Task<bool> IsAvailableForExtensionAsync(Guid rentId, DateTime newEndRentDate)
    {
        var rentEntity = await context.Rents.FindAsync(rentId);

        var thingId = rentEntity.ThingId;

        var overlappingRents = await context.Rents
            .Where(r => r.ThingId == thingId && r.Id != rentId 
                && r.StartRentDate < newEndRentDate && r.EndRentDate > rentEntity.EndRentDate)
            .ToListAsync();

        return !overlappingRents.Any();
    }
}
