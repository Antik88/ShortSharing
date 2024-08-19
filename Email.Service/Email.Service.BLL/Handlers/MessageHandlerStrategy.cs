using Email.Service.BLL.Interfaces;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Handlers;

public class MessageHandlerStrategy : IMessageHandlerStrategy
{
    private readonly Dictionary<MessageType, IMessageHandler> _messages = new();

    public MessageHandlerStrategy(IEnumerable<IMessageHandler> messageHandlers)
    {
        foreach (var messageHandler in messageHandlers)
        {
            _messages.Add(messageHandler.MessageType, messageHandler);
        }
    }

    public Task SendMessage(ConsumeContext<RentRecord> context)
    {
        if (!_messages.TryGetValue(context.Message.MessageType, out var messageHandler))
            throw new ArgumentException("type of message is not correct");

        return messageHandler.SendMessage(context.Message);
    }
}
