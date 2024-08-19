using Rent.Service.Domain.Entity;
using SharingMessages;

namespace Rent.Service.Application.Abstractions.Notification;

public interface IRentNotification
{
    Task SendRentMessage(RentEntity rent, MessageType type, CancellationToken cancellationToken);
}
