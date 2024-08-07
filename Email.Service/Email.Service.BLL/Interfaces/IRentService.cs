using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IRentService
{
    Task CreateRent(ConsumeContext<RentRecord> context);
    Task<List<RentEntity>> GetByRentStatus(RentStatus status);
}
