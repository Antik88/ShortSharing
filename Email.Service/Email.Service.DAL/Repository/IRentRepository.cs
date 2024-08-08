using MassTransit;
using SharingMessages;

namespace Email.Service.DAL.Repository;

public interface IRentRepository
{
    Task CreateRent(ConsumeContext<RentRecord> context);
}
