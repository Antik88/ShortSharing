using SharingMessages;

namespace Email.Service.BLL.Interfaces;

public interface IMessageHandlerFactory
{
    public IMessageHandler CreateHandler(MessageType messageType);
}
