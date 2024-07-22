
using Rent.Service.DAL.Entites;
using Rent.Service.DAL.Repositories.Interfaces;

namespace Rent.Service.DAL.Repositories.Implementations;

public class RentRepository : IRentRepository
{
    public Task AddAsync(RentEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<RentEntity> GetRange(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RentEntity>> GetRangeAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(RentEntity entityToRemove, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RentEntity newEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
