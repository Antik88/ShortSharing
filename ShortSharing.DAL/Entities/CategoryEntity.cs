using ShortSharing.DAL.Abstractions;
using System.Text.Json.Serialization;

namespace ShortSharing.DAL.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public required string Name { get; set; }

        [JsonIgnore]
        public List<TypeEntity>? Types { get; set; }
    }
}
