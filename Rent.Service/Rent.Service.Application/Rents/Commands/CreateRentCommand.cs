using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
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
    public Guid UserId { get; set; }
}

public class CreateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper,
    IRentNotification rentNotificationPublisher,
    IServiceConnection serviceConnection,
    IConfiguration configuration) : IRequestHandler<CreateRentCommand, RentModel>
{

    private readonly string? _catalogUrl = configuration
        .GetConnectionString("CatalogueConnection");

    private readonly string? _userServiceUrl = configuration
        .GetConnectionString("UserServiceConnection");

    public async Task<RentModel> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        var rentEntity = new RentEntity()
        {
            StartRentDate = request.StartRentDate,
            EndRentDate = request.EndRentDate,
            ThingId = request.ThingId,
            UserId = request.UserId,
        };

        if (_catalogUrl == null || _userServiceUrl == null)
            throw new Exception();

        var thingModel = await serviceConnection.GetFromServiceById<ThingModel>
            (request.ThingId, _catalogUrl, cancellationToken);

        var tenantModel = await serviceConnection.GetFromServiceById<UserModel>
            (request.UserId, _userServiceUrl, cancellationToken);

        var ownerModel = await serviceConnection.GetFromServiceById<UserModel>
            (request.UserId, _userServiceUrl, cancellationToken);
        //thingModel.OwnerId

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
