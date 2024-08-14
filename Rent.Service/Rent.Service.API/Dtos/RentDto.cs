using Rent.Service.Domain.Enums;

namespace Rent.Service.API.Dtos;

public class RentDto 
{
    public Guid Id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
    public Guid TenantId { get; set; }
    public RentStatus Status { get; set; }
}
