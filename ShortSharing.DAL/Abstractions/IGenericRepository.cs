namespace ShortSharing.DAL.Abstractions;

public interface IGenericRepository <TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> UpdateAsync(Guid id, TEntity entity);
    Task DeleteAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity entity);
}
