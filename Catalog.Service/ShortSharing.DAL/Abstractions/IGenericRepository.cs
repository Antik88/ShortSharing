namespace ShortSharing.DAL.Abstractions;

public interface IGenericRepository <TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token);
    Task<List<TEntity>> GetAllAsync(CancellationToken token);
    Task<TEntity?> UpdateAsync(Guid id, TEntity entity, CancellationToken token);
    Task DeleteAsync(Guid id, CancellationToken token);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken token);
}
