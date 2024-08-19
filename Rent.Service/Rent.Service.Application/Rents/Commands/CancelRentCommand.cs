using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Application.Model;
using SharingMessages;

namespace Rent.Service.Application.Rents.Commands;

public class CancelRentCommand : IRequest<RentModel>
{
    public Guid RentId { get; set; }
    public Guid TenantId { get; set; }
}

public class CancelRentCommandHandler(IRentManagementRepository rentRepository,
    IRentNotification notificationPublisher,
    IMapper mapper)
    : IRequestHandler<CancelRentCommand, RentModel>
{
    public async Task<RentModel> Handle(CancelRentCommand command, CancellationToken cancellationToken)
    {
        var rent = await rentRepository.CancelRent(command.RentId, command.TenantId);

        await notificationPublisher.SendRentMessage(rent, MessageType.RentStatusChange, cancellationToken);

        return mapper.Map<RentModel>(rent);
    }
}
