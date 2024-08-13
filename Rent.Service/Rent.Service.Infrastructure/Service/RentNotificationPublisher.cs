using MassTransit;
using MassTransit.Testing;
using MediatR;
using Rent.Service.Application;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Domain.Entity;
using SharingMessages;

namespace Rent.Service.Infrastructure.Service;

public class RentNotificationPublisher(
    IPublishEndpoint publisher,
    IExternalServiceRequests<ICatalogServiceHttpClient> catalogServiceRequests,
    IExternalServiceRequests<IUserServiceHttpClient> userServiceRequests)
    : IRentNotification
{
    public async Task SendRentMessage(RentEntity rent, MessageType type, CancellationToken cancellationToken)
    {
        var rentRecord = await GetRentRecord(rent, type, cancellationToken);

        await publisher.Publish(rentRecord, cancellationToken);
    }

    private async Task<RentRecord> GetRentRecord(RentEntity rent, MessageType type, CancellationToken cancellationToken)
    {
        var thingModel = await catalogServiceRequests.GetFromServiceById<ThingModel>(rent.ThingId, cancellationToken);

        var tenantModel = await userServiceRequests.GetFromServiceById<UserModel>(rent.TenantId, cancellationToken);

        var ownerModel = await userServiceRequests.GetFromServiceById<UserModel>(thingModel.OwnerId, cancellationToken);

        return new RentRecord(
            rent.Id,
            thingModel,
            ownerModel,
            tenantModel,
            rent.StartRentDate,
            rent.EndRentDate,
            type);
    }
}