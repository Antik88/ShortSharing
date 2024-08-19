using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IMessageHandlerStrategy
{
    Task SendMessage(ConsumeContext<RentRecord> context);
}
