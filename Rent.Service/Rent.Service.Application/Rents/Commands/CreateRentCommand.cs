﻿using AutoMapper;
using MediatR;
using Rent.Service.Application.Abstractions;
using Rent.Service.Application.Abstractions.Notification;
using Rent.Service.Application.Model;
using Rent.Service.Domain.Entity;
using Rent.Service.Infrastructure;
using SharingMessages;

namespace Rent.Service.Application.Rents.Commands;

public class CreateRentCommand : IRequest<RentModel>
{
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    public Guid ThingId { get; set; }
    public Guid UserId { get; set; }
}

public class CreateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper,
    IRentNotification rentNotificationPublisher,
    IExternalServiceRequests<ICatalogServiceHttpClient> catalogServiceRequests,
    IExternalServiceRequests<IUserServiceHttpClient> userServiceRequests
    ) : IRequestHandler<CreateRentCommand, RentModel>
{
    public async Task<RentModel> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        var rentEntity = new RentEntity()
        {
            StartRentDate = request.StartRentDate,
            EndRentDate = request.EndRentDate,
            ThingId = request.ThingId,
            UserId = request.UserId,
        };

        var thingModel = await catalogServiceRequests.GetFromServiceById<ThingModel>
            (request.ThingId, cancellationToken);

        var tenantModel = await userServiceRequests.GetFromServiceById<UserModel>
            (request.UserId, cancellationToken);

        var ownerModel = await userServiceRequests.GetFromServiceById<UserModel>
            (thingModel.OwnerId, cancellationToken);

        var result = await rentRepository.CreateAsync(rentEntity);

        await rentNotificationPublisher.SendRentMessage(new RentRecord(
            result.Id,
            thingModel,
            ownerModel,
            tenantModel, result.StartRentDate,
            result.EndRentDate));

        return mapper.Map<RentModel>(result);
    }
}
