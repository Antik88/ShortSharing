using AutoMapper;
using MediatR;
using Rent.Service.Domain.Entity;
using Rent.Service.Domain.Repository;

namespace Rent.Service.Application.Rents.Commands.UpdateRent;

public class UpdateRentCommandHandler(
    IRentRepository rentRepository,
    IMapper mapper) : IRequestHandler<UpdateRentCommand, int>
{
    public async Task<int> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
    {
        var rentEntity = new RentEntity()
        {
            Id = request.id,
            StartRentDate = request.StartRentDate,
            EndRentDate = request.EndRentDate,
            ThingId = request.ThingId,
            UserId = request.UserId
        };

        return await rentRepository.UpdateAsync(request.id, rentEntity);
    }
}
