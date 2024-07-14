using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.BLL.Abstractions;

public interface IThingsService 
{
    Task<ThingModel?> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedResult<ThingModel>> GetAllAsync(QueryParameters queryParameters, CancellationToken token);
    Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity, CancellationToken token);
    Task DeleteAsync(Guid id, CancellationToken token);
    Task<ThingModel> CreateAsync(ThingModel entity, CancellationToken token);
}
