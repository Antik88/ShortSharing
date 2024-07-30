using User.Service.DLL.Entities;
using User.Service.Shared;

namespace User.Service.DLL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<PageResult<UserEntity>> GetRangeAsync(Query query, CancellationToken cancellationToken);

    Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<UserEntity> AddAsync(UserEntity userEntity, CancellationToken cancellationToken);

    Task<UserEntity> UpdateAsync(Guid id, UserEntity userEntity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}

