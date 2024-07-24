using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Rents.CommandsHandlers;

public class UpdateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper) : IRequestHandler<UpdateRentCommand, int>
{
    public async Task<int> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var rentEntity = new RentEntity()
        {
            Id = request.id,
            StartRentDate = request.StartRentDate,
            EndRentDate = request.EndRentDate,
            UserId = request.UserId
        };

        return await rentRepository.UpdateAsync(request.id, rentEntity);
    }
}
