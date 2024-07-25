
using AutoMapper;
using MediatR;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Application.Rents.CommandsHandlers;

public class DeleteRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper) : IRequestHandler<DeleteRentCommand>
{
    public async Task Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        await rentRepository.DeleteAsync(request.Id);
    }
}
