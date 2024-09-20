using ShortSharing.BLL.Models;

namespace ShortSharing.BLL.Abstractions;

public interface ICategoryService
{
    Task<CategoryModel?> GetById(Guid id, CancellationToken token);
    Task<List<CategoryModel>> GetAll(CancellationToken token);
    Task<CategoryModel?> Update(Guid id, CategoryModel model, CancellationToken token);
    Task<CategoryModel> Delete(Guid id, CancellationToken token);
    Task<CategoryModel> Create(CategoryModel categoryModel, CancellationToken token);
}
