﻿using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Context;
using Email.Service.DAL.Entities;
using MassTransit;
using MongoDB.Driver;
using SharingMessages;

namespace Email.Service.BLL.Service;

public class RentService : IRentService
{
    private readonly IMongoCollection<RentEntity> _rents;

    public RentService(MongoDbService mongoDbService)
    {
        _rents = mongoDbService.Database.GetCollection<RentEntity>("rents");
    }

    public Task CreateRent(ConsumeContext<RentRecord> context)
    {
        RentEntity entity = new RentEntity()
        {
            StartDate = context.Message.StartDate,
            EndDate = context.Message.EndDate,
            ThingName = context.Message.Thing.Name,
            OwnerEmail = context.Message.Owner.Email,
            OwnerName = context.Message.Owner.Name,
            TenantEmail = context.Message.Tenant.Email,
            TenantName = context.Message.Tenant.Name,
        };

        return _rents.InsertOneAsync(entity);
    }
}
