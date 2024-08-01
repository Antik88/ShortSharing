using SharingMessages;

namespace Rent.Service.Application.Abstractions.Notification;

public interface IRentNotification
{
    Task SendRentMessage(RentRecord rentRecord);
}
