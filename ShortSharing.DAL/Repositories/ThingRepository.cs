using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.DAL.Repositories;
public class ThingRepository : IThingRepository
{
    private readonly ApplicationDbContext _context;

    public ThingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ThingEntity> CreateAsync(ThingEntity entity, CancellationToken token)
    {
        await _context.AddAsync(entity, token);

        await _context.SaveChangesAsync(token);

        return entity;
    }

    public async Task<PagedResult<ThingEntity>> GetAllAsync(QueryParameters queryParameters, CancellationToken token)
    {
        IQueryable<ThingEntity> query = _context.Things
            .Include(t => t.Category)
            .Include(t => t.Type)
            .Include(t => t.Owner);

        if (queryParameters.CategoryId.HasValue)
        {
            query = query.Where(t => t.Category.Id == queryParameters.CategoryId.Value);
        }

        if (queryParameters.TypeId.HasValue)
        {
            query = query.Where(t => t.Type.Id == queryParameters.TypeId.Value);
        }

        int totalItems = await query.CountAsync(token);

        List<ThingEntity> items = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                                             .Take(queryParameters.PageSize)
                                             .ToListAsync(token);

        return new PagedResult<ThingEntity>(items, totalItems, queryParameters.PageNumber, queryParameters.PageSize);
    }
}
