using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class ThingEntity : BaseEntity, IAuditableEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required CategoryEntity Category { get; set; }
        public required TypeEntity Type { get; set; }
        public required Guid OwnerId { get; set; }
    }
}
