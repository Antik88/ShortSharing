using ShortSharing.BLL.Abstractions;
using System.Text.Json.Serialization;

namespace ShortSharing.BLL.Models;

public class UserModel : BaseModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    [JsonIgnore]
    public List<ThingModel>? Things { get; set; }

    [JsonIgnore]
    public List<RentModel>? Rents { get; set; }
}
