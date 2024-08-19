﻿using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;

namespace ShortSharing.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext context;
    private readonly DbSet<TEntity> dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken token)
    {
        return await dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        var entity = await dbSet.FindAsync(id);

        if (entity != null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<TEntity?> UpdateAsync(Guid id, TEntity entity, CancellationToken token)
    {
        var existEntity = dbSet.Find(id);

        if (existEntity == null)
        {
            return null;
        }

        context.Entry(existEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();

        return existEntity;
    }
}
