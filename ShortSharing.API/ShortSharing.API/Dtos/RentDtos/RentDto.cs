using ShortSharing.API.Dtos.ThingDtos;

namespace ShortSharing.API.Dtos.RentDtos;

public class RentDto
{
    public DateOnly StartRentDate { get; set; }
    public DateOnly EndRentDate { get; set; }
    public required ThingDto Thing { get; set; }
    public required RentDto Renter { get; set; }
}
