using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Application.Model;
using Rent.Service.Domain.Entity;
using SharingMessages;

namespace Rent.Service.Application.Rents.Commands;

public class CreateRentCommand : IRequest<RentModel>
{
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
    public Guid TenantId { get; set; }
}

public class CreateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper,
    IRentNotification rentNotificationPublisher
    ) : IRequestHandler<CreateRentCommand, RentModel>
{
    public async Task<RentModel> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        var rentEntity = new RentEntity()
        {
            StartRentDate = request.StartRentDate,
            EndRentDate = request.EndRentDate,
            ThingId = request.ThingId,
            TenantId = request.TenantId,
        };

        var rent = await rentRepository.CreateAsync(rentEntity);

        await rentNotificationPublisher.SendRentMessage(rent, MessageType.NewRent, cancellationToken);
        
        return mapper.Map<RentModel>(rent);
    }
}
