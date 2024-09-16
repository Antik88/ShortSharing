using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.DAL.Abstractions;

public interface IThingRepository
{
    Task<ThingEntity> CreateAsync(ThingEntity entity, CancellationToken token);
    Task<PagedResult<ThingEntity>> GetAllAsync(QueryParameters queryParameters, CancellationToken token);
    Task<ThingEntity> GetById(Guid id, CancellationToken token);
}
