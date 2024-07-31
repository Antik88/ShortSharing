using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IRentService
{
    Task CreateRent(ConsumeContext<RentRecord> context);
}
