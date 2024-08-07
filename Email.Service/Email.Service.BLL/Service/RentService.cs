using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Context;
using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using MassTransit;
using MongoDB.Driver;
using SharingMessages;

namespace Email.Service.BLL.Service;

public class RentService : IRentService
{
    private readonly IMongoCollection<RentEntity> _rents;

    public RentService(DbContext dbContext)
    {
        _rents = dbContext.Database.GetCollection<RentEntity>("rents");
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

    public async Task<List<RentEntity>> GetByRentStatus(RentStatus status)
    {
        var filter = Builders<RentEntity>.Filter.Eq(t => t.Status, status);

        var result = await _rents.Find(filter).ToListAsync();

        return result; 
    }
}
