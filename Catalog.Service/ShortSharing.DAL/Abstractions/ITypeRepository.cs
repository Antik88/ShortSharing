using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Abstractions;

public interface ITypeRepository
{
    Task<TypeEntity> CreateAsync(TypeEntity entity, CancellationToken token);
}
