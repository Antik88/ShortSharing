using ShortSharing.BLL.Abstractions;
using ShortSharing.DAL.Entities;
using System.Text.Json.Serialization;

namespace ShortSharing.BLL.Models;

public class CategoryModel : BaseModel
{
    public required string Name { get; set; }

    public List<TypeModel>? Types { get; set; }
}
