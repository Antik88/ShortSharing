using MediatR;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Commands;

public class ExtendRentCommand : IRequest<RentModel>
{
    public Guid RentId { get; set; }
    public DateTime NewEndRentDate { get; set; }
}
