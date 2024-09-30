using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities;

public class ImageEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid ThingId { get; set; }
    public ThingEntity Thing { get; set; } 
}
