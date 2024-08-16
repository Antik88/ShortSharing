using Microsoft.EntityFrameworkCore;
using Rent.Service.Domain.Entity;
using Rent.Service.Application.Abstractions;
using Rent.Service.Infrastructure.Data;
using Rent.Service.Domain.Enums;
using Rent.Service.Application.Common.Exceptions;
using Rent.Service.Application.Common.Constants;

namespace Rent.Service.Infrastructure.Repository;

public class RentRepository(RentDbContext context) : IRentManagementRepository,
    IRentQueryRepository,
    IRentAvailabilityRepository,
    IRentExtensionRepository,
    IRentStatusChanger,
    ICancelRentRepository
{
    public async Task<RentEntity> CreateAsync(RentEntity rentEntity)
    {
        if (rentEntity.StartRentDate.Date == DateTime.Now.Date)
            rentEntity.Status = RentStatus.Active;

        await context.Rents.AddAsync(rentEntity);

        await context.SaveChangesAsync();

        return rentEntity;
    }

    public Task DeleteAsync(Guid id)
    {
        return context.Rents
           .Where(model => model.Id == id)
           .ExecuteDeleteAsync();
    }

    public Task<List<RentEntity>> GetAllRentsAsync()
    {
        return context.Rents.ToListAsync();
    }

    public async Task<RentEntity> GetByIdAsync(Guid id)
    {
        var result = await context.Rents.AsNoTracking()
            .FirstOrDefaultAsync(model => model.Id == id);

        return result;
    }

    public async Task<List<RentEntity>> GetByUserId(Guid userId)
    {
        var result = await context.Rents.AsNoTracking()
            .Where(model => model.TenantId == userId).ToListAsync();

        return result;
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

        return existingRents
            .Where(rent => rent.Status != RentStatus.Canceled)
            .All(rent =>
                rent.EndRentDate <= startRentDate ||
                rent.StartRentDate >= endRentDate);
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

    public async Task<List<RentEntity>> GetNotExpiredRents()
    {
        var rents = await context.Rents
            .Where(rent => rent.Status == RentStatus.Pending || rent.Status == RentStatus.Active)
            .ToListAsync();

        return rents;
    }

    public async Task<bool> IsStatusChanged(RentEntity rent)
    {
        bool statusChanged = false;
        var currentDate = DateTime.UtcNow.Date;

        if (rent.StartRentDate.Date <= currentDate
            && rent.EndRentDate.Date >= currentDate
            && rent.Status != RentStatus.Active)
        {
            await ChangeStatus(rent.Id, RentStatus.Active);
            statusChanged = true;
        }
        else if (rent.EndRentDate.Date < currentDate)
        {
            await ChangeStatus(rent.Id, RentStatus.Expired);
            statusChanged = true;
        }

        return statusChanged;
    }


    private async Task<RentEntity> ChangeStatus(Guid id, RentStatus status)
    {
        var rent = await context.Rents.FindAsync(id);

        rent.Status = status;

        await context.SaveChangesAsync();

        return rent;
    }

    public async Task<RentEntity> CancelRent(Guid rentId, Guid tenantId)
    {
        var rent = await context.Rents.FindAsync(rentId)
            ?? throw new InvalidRequestException([ValidationMessages.RentIdInvalid]);

        if (rent.TenantId != tenantId)
            throw new InvalidRequestException([ValidationMessages.TenantIdNotEqual]);

        rent.Status = RentStatus.Canceled;

        await context.SaveChangesAsync();

        return rent;
    }
}
