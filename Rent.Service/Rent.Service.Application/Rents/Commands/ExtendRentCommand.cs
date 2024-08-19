using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Model;

namespace Rent.Service.Application.Rents.Commands;

public class ExtendRentCommand : IRequest<RentModel>
{
    public Guid RentId { get; set; }
    public DateTime NewEndRentDate { get; set; }
}

public class ExtendRentCommandHandler(IRentExtensionRepository rentRepository) 
    : IRequestHandler<ExtendRentCommand, RentModel>
{
    public async Task<RentModel> Handle(ExtendRentCommand request, CancellationToken cancellationToken)
    {
        var updatedRent = await rentRepository.ExtendRentAsync(request.RentId, request.NewEndRentDate);

        return new RentModel
        {
            Id = updatedRent.Id,
            ThingId = updatedRent.ThingId,
            TenantId = updatedRent.TenantId,
            StartRentDate = updatedRent.StartRentDate,
            EndRentDate = updatedRent.EndRentDate
        };
    }
}
