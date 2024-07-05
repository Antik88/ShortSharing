using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Abstractions;

public interface IThingsService 
{
    Task<ThingModel?> GetByIdAsync(Guid id);
    Task<List<ThingModel>> GetAllAsync();
    Task<ThingModel?> UpdateAsync(Guid id, ThingEntity entity);
    Task DeleteAsync(Guid id);
    Task<ThingModel> CreateAsync(ThingModel entity);
}
