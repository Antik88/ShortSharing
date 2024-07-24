using Rent.Service.Application.Model;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;

public interface IRentRepository
{
    Task<List<RentEntity>> GetAllRentsAsync();
    Task<List<RentEntity>> GetByUserId(Guid userId);
    Task<RentEntity> GetByIdAsync(Guid id);
    Task<RentEntity> CreateAsync(RentEntity rentEntity);
    Task<int> UpdateAsync(Guid id, RentEntity rentEntity);
    Task<int> DeleteAsync(Guid id);
    Task<IEnumerable<RentEntity>> GetRentsForThingAsync(Guid thingId);
    Task<bool> IsAvailableAsync(Guid thingId, DateTime startRentDate, DateTime endRentDate);
    Task<RentEntity> ExtendRentAsync(Guid rentId, DateTime newEndRentDate);
    Task<bool> IsAvailableForExtensionAsync(Guid rentId, DateTime newEndRentDate);
}
