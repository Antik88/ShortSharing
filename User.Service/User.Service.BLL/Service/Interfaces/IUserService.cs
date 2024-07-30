using User.Service.BLL.Models;
using User.Service.Shared;

namespace User.Service.BLL.Service.Interfaces;

public interface IUserService
{
    Task<PageResult<UserModel>> GetRangeAsync(Query query, CancellationToken cancellationToken);

    Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<UserModel> AddAsync(UserModel userModel, CancellationToken cancellationToken);

    Task<UserModel> UpdateAsync(Guid id, UserModel userModel, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}
