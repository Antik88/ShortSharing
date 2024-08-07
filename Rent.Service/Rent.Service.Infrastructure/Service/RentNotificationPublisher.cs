using MassTransit;
using Rent.Service.Application.Abstractions.Notification;
using SharingMessages;

namespace Rent.Service.Infrastructure.Service;

public class RentNotificationPublisher(IPublishEndpoint publisher)
    : IRentNotification
{
    public async Task SendRentMessage(RentRecord rentRecord)
    {
        await publisher.Publish(rentRecord);
    }
}
