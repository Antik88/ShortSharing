using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class TypeEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required CategoryEntity Category { get; set; }
    }
}
