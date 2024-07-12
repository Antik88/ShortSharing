using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Abstractions;

public interface IThingRepository
{
    Task<ThingEntity> CreateAsync(ThingEntity entity, CancellationToken token);
    Task<IEnumerable<ThingEntity>> GetAllAsync(CancellationToken token, int pageNumber, int pageSize, Guid? categoryId, Guid? typeId);
}
