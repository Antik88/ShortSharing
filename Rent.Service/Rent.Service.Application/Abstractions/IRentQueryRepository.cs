﻿using Rent.Service.Application.Model;
using Rent.Service.Domain.Entity;

namespace Rent.Service.Application.Abstractions;
public interface IRentQueryRepository
{
    Task<List<RentEntity>> GetAllRentsAsync();
    Task<List<RentEntity>> GetByUserId(Guid userId);
    Task<List<RentEntity>> GetNotExpiredRents();
    Task<RentEntity> GetByIdAsync(Guid id);
    Task<List<RentEntity>> GetByThingId(Guid id);
    Task<IEnumerable<RentEntity>> GetRentsForThingAsync(Guid thingId);
}
