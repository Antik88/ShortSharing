using ShortSharing.BLL.Models;
using ShortSharing.DAL.Abstractions;

namespace ShortSharing.BLL.Abstractions;

public interface IThingsService 
{
    Task<ThingModel?> GetByIdAsync(Guid id);
    Task<List<ThingModel>> GetAllAsync();
    Task<ThingModel?> UpdateAsync(Guid id, ThingModel entity);
    Task DeleteAsync(Guid id);
    Task<ThingModel> CreateAsync(ThingModel entity);
}
