
using Rent.Service.BLL.Models;

namespace Rent.Service.BLL.Services.Interfaces;

public interface IRentService
{
    Task<RentModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<RentModel> AddAsync(RentModel rentModel, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
