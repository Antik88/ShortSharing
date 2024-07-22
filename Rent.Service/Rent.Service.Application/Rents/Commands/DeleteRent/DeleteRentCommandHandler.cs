
using AutoMapper;
using MediatR;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.Commands.DeleteRent;

public class DeleteRentCommandHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<DeleteRentCommand, Guid>
{
    public async Task<Guid> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
    {
        return await rentRepository.DeleteAsync(request.Id);
    }
}
