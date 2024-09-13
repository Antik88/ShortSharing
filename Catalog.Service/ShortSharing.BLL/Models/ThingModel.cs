using ShortSharing.BLL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Models;

public class ThingModel : BaseModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required CategoryModel Category { get; set; }
    public required TypeModel Type { get; set; }
    public Guid OwnerId { get; set; }
    public List<ImageModel> Images { get; set; }
}
