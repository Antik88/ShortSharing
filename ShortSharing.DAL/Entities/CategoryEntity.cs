using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class CategoryEntity : IBaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Type>? Types { get; set; }
    }
}
