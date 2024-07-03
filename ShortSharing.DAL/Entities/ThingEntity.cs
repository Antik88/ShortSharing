using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class ThingEntity : BaseEntity
    {
        public required string Name { get; set; } 
        public required string Description { get; set; } 
        public required double Price { get; set; } 
        public required CategoryEntity Category { get; set; } 
        public required TypeEntity Type { get; set; } 
        public required UserEntity Owner { get; set; } 
    }
}
