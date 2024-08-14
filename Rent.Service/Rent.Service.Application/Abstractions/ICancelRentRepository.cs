using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;

public interface ICancelRentRepository
{
    Task<RentEntity> CancelRent(Guid rentId, Guid tenantId);
}
