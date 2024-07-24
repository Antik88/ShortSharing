﻿using AutoMapper;
using MediatR;
using Rent.Service.Application.Model;
using Rent.Service.Application.Rents.Commands;
using Rent.Service.Domain.Entity;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Application.Rents.CommandsHandlers;

public class CreateRentCommandHandler(
    IRentManagementRepository rentRepository,
    IMapper mapper) : IRequestHandler<CreateRentCommand, RentModel>
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

        var result = await rentRepository.CreateAsync(rentEntity);

        return mapper.Map<RentModel>(result);
    }
}
