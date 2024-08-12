using Microsoft.EntityFrameworkCore;
using User.Service.DLL.Context;
using User.Service.DLL.Entities;
using User.Service.DLL.Repositories.Interfaces;
using User.Service.Shared;

namespace User.Service.DLL.Repositories.Implementation;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<UserEntity> AddAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        await context.AddAsync(userEntity, cancellationToken);

        await context.SaveChangesAsync();

        return userEntity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new ArgumentException($"Entity with id {id} not found.");
        }

        context.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstAsync(u => u.Id == id, cancellationToken);

        return user;
    }

    public async Task<PageResult<UserEntity>> GetRangeAsync(Query query, CancellationToken cancellationToken)
    {
        IQueryable<UserEntity> contextQuery = context.Users;

        List<UserEntity> items = await contextQuery.Skip((query.Page - 1) * query.PageSize)
                                             .Take(query.PageSize)
                                             .ToListAsync(cancellationToken);
        
        return new PageResult<UserEntity>
        {
            Items = items,
            CurrentPage = query.Page,
            PageSize = query.PageSize,
        };
    }

    public async Task<UserEntity> UpdateAsync(Guid id, UserEntity userEntity, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.FindAsync(id);

        if (existingUser == null)
        {
            throw new ArgumentException($"User with id {id} not found.");
        }

        existingUser.Name = userEntity.Name;
        existingUser.Email = userEntity.Email;

        await context.SaveChangesAsync(cancellationToken);

        return existingUser;
    }
}
