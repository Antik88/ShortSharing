using Microsoft.EntityFrameworkCore;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Entities;

namespace ShortSharing.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryEntity>> GetAllCategories(CancellationToken token)
    {
        var result = await _context.Categories.Include(c => c.Types).ToListAsync();

        return result;
    }
}
