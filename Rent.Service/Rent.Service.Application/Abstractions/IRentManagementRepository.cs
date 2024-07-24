using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;

public interface IRentManagementRepository
{
    Task<RentEntity> CreateAsync(RentEntity rentEntity);
    Task<int> UpdateAsync(Guid id, RentEntity rentEntity);
    Task<int> DeleteAsync(Guid id);
}
