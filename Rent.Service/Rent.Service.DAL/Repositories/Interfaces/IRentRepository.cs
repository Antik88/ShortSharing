using Rent.Service.DAL.Entites;

namespace Rent.Service.DAL.Repositories.Interfaces;

public interface IRentRepository : IBaseRepository<RentEntity>
{
    Task<IEnumerable<RentEntity>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken);
}
