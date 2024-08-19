using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;

public interface IRentStatusChanger
{
    Task<bool> IsStatusChanged(RentEntity rent);
}
