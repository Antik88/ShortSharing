using ShortSharing.DAL.Abstractions;
using System.Text.Json.Serialization;

namespace ShortSharing.DAL.Entities
{
    public class UserEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }

        [JsonIgnore]
        public List<ThingEntity>? Things { get; set; }

        [JsonIgnore]
        public List<RentEntity>? Rents { get; set; }
    }
}
