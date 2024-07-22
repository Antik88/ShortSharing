namespace Rent.Service.DAL.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> GetRange(int page, int pageSize);

    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(T newEntity, CancellationToken cancellationToken);

    Task RemoveAsync(T entityToRemove, CancellationToken cancellationToken);
}
