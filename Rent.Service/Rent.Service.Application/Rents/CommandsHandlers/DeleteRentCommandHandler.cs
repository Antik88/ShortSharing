
using AutoMapper;
using MediatR;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Application.Rents.CommandsHandlers;

public class DeleteRentCommandHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<DeleteRentCommand, int>
{
    public async Task<int> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        return await rentRepository.DeleteAsync(request.Id);
    }
}
