using ShortSharing.DAL.Abstractions;

namespace ShortSharing.DAL.Entities
{
    public class RentEntity : BaseEntity
    {
        public DateOnly StartRentDate { get; set; }
        public DateOnly EndRentDate { get; set; }
        public required ThingEntity Thing { get; set; }
        public required UserEntity Renter { get; set; }
    }
}
