using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IMessageHandler
{
    Task Handle(ConsumeContext<RentRecord> context);
}
