using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Abstractions;

public interface IThingsService 
{
    Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token);
    Task<List<ThingModel>> GetAllAsync(CancellationToken token, int pageNumber, int pageSize, Guid? categoryId, Guid? typeId);
    Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity, CancellationToken token);
    Task DeleteAsync(Guid id, CancellationToken token);
    Task<ThingModel> CreateAsync(ThingModel entity, CancellationToken token);
}
