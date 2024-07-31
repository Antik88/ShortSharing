using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;

public interface IRentExtensionRepository
{
    Task<RentEntity> ExtendRentAsync(Guid rentId, DateTime newEndRentDate);
}
