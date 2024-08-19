using ShortSharing.BLL.Abstractions;

namespace ShortSharing.BLL.Models;

public class TypeModel : BaseModel
{
    public required string Name { get; set; }
    public required CategoryModel Category { get; set; }
}
