using MediatR;
using Rent.Service.Application.Rents.Queries.GetRents;

namespace Rent.Service.Application.Rents.Commands.CreateRent;

public class CreateRent : IRequest<RentModel>
{
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
    public Guid UserId { get; set; }
}
