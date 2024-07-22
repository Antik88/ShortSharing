using Rent.Service.Domain.Entity;

namespace Rent.Service.Domain.Repository;

public interface IRentRepository 
{
    Task<List<RentEntity>> GetAllRentsAsync();
    Task<RentEntity> GetByIdAsync(Guid id);
    Task<RentEntity> CreateAsync(RentEntity rentEntity);
    Task<Guid> UpdateAsync(Guid id, RentEntity rentEntity);
    Task<Guid> DeleteAsync(Guid id);
}
