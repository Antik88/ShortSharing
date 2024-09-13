using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;
using ShortSharing.Shared;

namespace ShortSharing.DAL.Repositories;

public class ThingRepository(ApplicationDbContext context) : IThingRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ThingEntity> CreateAsync(ThingEntity entity, CancellationToken token)
    {
        entity.Category = _context.Categories.Find(entity.Category.Id); 

        entity.Type = _context.Types.Find(entity.Type.Id); 

        await _context.Things.AddAsync(entity, token);

        await _context.SaveChangesAsync(token);

        return entity;
    }

    public async Task<PagedResult<ThingEntity>> GetAllAsync(QueryParameters queryParameters, CancellationToken token)
    {
        IQueryable<ThingEntity> query = _context.Things
            .Include(t => t.Category)
            .Include(t => t.Type)
            .Include(t => t.Images);

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

        return new PagedResult<ThingEntity> {
            Items = items,
            TotalCount = totalItems,
            CurrentPage = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize
        };
    }
}
