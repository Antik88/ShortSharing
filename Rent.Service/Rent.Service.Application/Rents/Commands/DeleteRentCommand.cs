using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Application.Rents.Commands;

public class DeleteRentCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper) : IRequestHandler<DeleteRentCommand>
{
    public async Task Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        await rentRepository.DeleteAsync(request.Id);
    }
}
