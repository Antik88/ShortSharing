using ShortSharing.BLL.Abstractions;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Models;

public class RentModel : BaseModel
{
    public DateOnly StartRentDate { get; set; }
    public DateOnly EndRentDate { get; set; }
    public required ThingEntity Thing { get; set; }
    public required UserEntity Renter { get; set; }
}
