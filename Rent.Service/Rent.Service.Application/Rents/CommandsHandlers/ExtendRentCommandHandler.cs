using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Commands;

namespace Rent.Service.Application.Rents.CommandsHandlers;

public class ExtendRentCommandHandler(IRentRepository rentRepository) 
    : IRequestHandler<ExtendRentCommand, RentModel>
{
    public async Task<RentModel> Handle(ExtendRentCommand request, CancellationToken cancellationToken)
    {
        var updatedRent = await rentRepository.ExtendRentAsync(request.RentId, request.NewEndRentDate);

        return new RentModel
        {
            Id = updatedRent.Id,
            ThingId = updatedRent.ThingId,
            UserId = updatedRent.UserId,
            StartRentDate = updatedRent.StartRentDate,
            EndRentDate = updatedRent.EndRentDate
        };
    }
}
