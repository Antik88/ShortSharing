using Email.Service.BLL.Interfaces;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Consumers;

public class RentConsumer(IMessageHandlerFactory handlerFactory) : IConsumer<RentRecord>
{
    public Task Consume(ConsumeContext<RentRecord> context)
    {
        var handler = handlerFactory.CreateHandler(context.Message.MessageType);

        handler.Handle(context);

        return Task.CompletedTask;
    }
}
