using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly ApplicationDbContext _context;

    public TypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TypeEntity> CreateAsync(TypeEntity entity, CancellationToken token)
    {
        entity.Category = _context.Categories.Find(entity.Category.Id);

        await _context.AddAsync(entity, token);

        await _context.SaveChangesAsync(token);

        return entity;
    }
}
