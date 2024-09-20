using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Abstractions;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAllCategories(CancellationToken token);
}
