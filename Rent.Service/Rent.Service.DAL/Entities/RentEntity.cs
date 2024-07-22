
namespace Rent.Service.DAL.Entites;
public class RentEntity
{
    public Guid Id { get; set; }
    public Guid ThingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
