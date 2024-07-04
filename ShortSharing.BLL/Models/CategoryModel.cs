using ShortSharing.BLL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Models
{
    public class CategoryModel : BaseModel
    {
        public required string Name { get; set; }
        public List<TypeEntity>? Types { get; set; }
    }
}
