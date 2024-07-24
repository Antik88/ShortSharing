using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;
public interface IRentQueryRepository
{
    Task<List<RentEntity>> GetAllRentsAsync();
    Task<List<RentEntity>> GetByUserId(Guid userId);
    Task<RentEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<RentEntity>> GetRentsForThingAsync(Guid thingId);
}