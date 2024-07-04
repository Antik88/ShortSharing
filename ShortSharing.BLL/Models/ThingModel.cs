using ShortSharing.BLL.Abstractions;

namespace ShortSharing.BLL.Models
{
    public class ThingModel : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required CategoryModel Category { get; set; }
        public required TypeModel Type { get; set; }
        public required UserModel Owner { get; set; }

    }
}
