using AutoMapper;
using Azure;
using User.Service.BLL.Models;
using User.Service.BLL.Service.Interfaces;
using User.Service.DLL.Entities;
using User.Service.DLL.Repositories.Interfaces;
using User.Service.Shared;

namespace User.Service.BLL.Service.Implementation;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<UserModel> AddAsync(UserModel userModel, CancellationToken cancellationToken)
    {
        var userEntity = mapper.Map<UserEntity>(userModel);

        var result = await userRepository.AddAsync(userEntity, cancellationToken);

        return mapper.Map<UserModel>(result);
    }

    public Task Delete(Guid id, CancellationToken cancellationToken)
    {
        return userRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<UserModel> GetByAuth0Id(string id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByAuth0Id(id, cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    public async Task<PageResult<UserModel>> GetRangeAsync(Query query, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetRangeAsync(query, cancellationToken);

        var userModel = mapper.Map<List<UserModel>>(result.Items);

        return new PageResult<UserModel>
        {
            Items = userModel,
            PageSize = query.PageSize,
            CurrentPage = query.Page
        };
    }

    public async Task<UserModel> UpdateAsync(Guid id, UserModel userModel, CancellationToken cancellationToken)
    {
        var result = await userRepository.UpdateAsync(id, 
            mapper.Map<UserEntity>(userModel), cancellationToken);

        return mapper.Map<UserModel>(result);
    }
}
