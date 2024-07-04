using ShortSharing.BLL.Abstractions;

namespace ShortSharing.BLL.Models
{
    public class UserModel : BaseModel
    {
        public required string Name { get; set; } 
        public required string Email { get; set; } 
        public DateOnly DateOfBirth { get; set; } 
        public List<ThingModel>? Things { get; set; } 
        public List<RentModel>? Rents { get; set; } 
    }
}
