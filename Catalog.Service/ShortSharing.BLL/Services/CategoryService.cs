using AutoMapper;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Services;

public class CategoryService(IMapper mapper, IGenericRepository<CategoryEntity> repository,
    ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<CategoryModel> Create(CategoryModel model, CancellationToken token)
    {
        var entity = mapper.Map<CategoryEntity>(model);

        var result = await repository.CreateAsync(entity, token);

        return mapper.Map<CategoryModel>(result);
    }

    public async Task<CategoryModel> Delete(Guid id, CancellationToken token)
    {
        var result = await repository.GetByIdAsync(id, token);

        return mapper.Map<CategoryModel>(result);
    }

    public async Task<List<CategoryModel>> GetAll(CancellationToken token)
    {
        var result = await categoryRepository.GetAllCategories(token);

        return mapper.Map<List<CategoryModel>>(result);
    }

    public async Task<CategoryModel?> GetById(Guid id, CancellationToken token)
    {
        var result = await repository.GetByIdAsync(id, token);

        return mapper.Map<CategoryModel>(result);
    }

    public async Task<CategoryModel?> Update(Guid id, CategoryModel model, CancellationToken token)
    {
        var entity = mapper.Map<CategoryEntity>(model);

        var result = await repository.UpdateAsync(id, entity, token);

        return mapper.Map<CategoryModel>(result);
    }
}
