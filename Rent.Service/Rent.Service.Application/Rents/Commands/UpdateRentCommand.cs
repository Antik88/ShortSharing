using MediatR;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Commands;

public class UpdateRentCommand : IRequest<RentModel>
{
    public Guid Id { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
}
