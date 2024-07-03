using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class TypeEntity : IBaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required CategoryEntity Category { get; set; }
    }
}
