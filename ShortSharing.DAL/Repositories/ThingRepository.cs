using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;


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

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<ThingEntity>> GetAllAsync(CancellationToken token, int pageNumber, int pageSize, Guid? categoryId, Guid? typeId)
    {
        IQueryable<ThingEntity> query = _context.Things
          .Include(t => t.Category.Id)
          .Include(t => t.Type.Id)
          .Include(t => t.Owner.Id);

        if (categoryId.HasValue)
        {
            query = query.Where(t => t.Category.Id == categoryId.Value);
        }

        if (typeId.HasValue)
        {
            query = query.Where(t => t.Type.Id == typeId.Value);
        }

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync(token);
    }
}
