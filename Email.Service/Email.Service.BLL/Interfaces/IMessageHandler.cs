using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IMessageHandler
{
    Task SendMessage(RentRecord message);

    MessageType MessageType { get; }
}
