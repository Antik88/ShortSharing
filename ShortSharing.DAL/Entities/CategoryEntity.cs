using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public required string Name { get; set; }
        public List<TypeEntity>? Types { get; set; }
    }
}
