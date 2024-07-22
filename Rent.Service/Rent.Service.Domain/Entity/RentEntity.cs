
namespace Rent.Service.Domain.Entity;

public class RentEntity
{
    public Guid Id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
}
