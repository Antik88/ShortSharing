
using MediatR;

namespace Rent.Service.Application.Rents.Commands.DeleteRent;

public class DeleteRentCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}
