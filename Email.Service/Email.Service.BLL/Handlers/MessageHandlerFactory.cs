using Email.Service.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SharingMessages;

namespace Email.Service.BLL.Handlers;

public class MessageHandlerFactory(IServiceProvider serviceProvider) : IMessageHandlerFactory
{
    public IMessageHandler CreateHandler(MessageType messageType)
    {
        return messageType switch
        {
            MessageType.NewRent => serviceProvider.GetRequiredService<NewRentMessageHandler>(),
            MessageType.RentStatusChange => serviceProvider.GetRequiredService<StatusChangeMessageHandler>(),
            _ => throw new NotImplementedException($"Handler for message type {messageType} is not implemented.")
        };
    }
}
