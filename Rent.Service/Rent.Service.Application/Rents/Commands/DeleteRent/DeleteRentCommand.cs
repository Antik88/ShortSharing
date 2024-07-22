
using MediatR;

namespace Rent.Service.Application.Rents.Commands.DeleteRent;

public class DeleteRentCommand : IRequest<int>
{
    public Guid Id { get; set; }
}
