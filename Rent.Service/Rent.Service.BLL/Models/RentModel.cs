
namespace Rent.Service.BLL.Models;

public class RentModel
{
    public Guid Id { get; set; }
    public Guid ThingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
