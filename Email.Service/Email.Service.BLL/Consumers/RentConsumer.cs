using Email.Service.BLL.Interfaces;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Consumers;

public class RentConsumer(IMessageHandlerStrategy messageHandlerStrategy) : IConsumer<RentRecord>
{
    public Task Consume(ConsumeContext<RentRecord> context)
    {
        messageHandlerStrategy.SendMessage(context);

        return Task.CompletedTask;
    }
}
