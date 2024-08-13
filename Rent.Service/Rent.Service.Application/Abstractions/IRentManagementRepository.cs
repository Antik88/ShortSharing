using MediatR;
using Rent.Service.Domain.Entity;
using Rent.Service.Domain.Enums;

namespace Rent.Service.Application.Abstractions;

public interface IRentManagementRepository
{
    Task<RentEntity> CreateAsync(RentEntity rentEntity);
    Task<RentEntity> UpdateAsync(Guid id, DateTime startRentDate, DateTime endRentDate);
    Task DeleteAsync(Guid id);
}
