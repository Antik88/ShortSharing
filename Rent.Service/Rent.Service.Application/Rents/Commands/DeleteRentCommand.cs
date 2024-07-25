using MediatR;

namespace Rent.Service.Application.Rents.Commands;

public class DeleteRentCommand : IRequest
{
    public Guid Id { get; set; }
}
