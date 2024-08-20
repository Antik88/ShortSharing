using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Enums;
using Email.Service.Interfaces;
using Email.Service.Shared;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Handlers;

public class StatusChangeMessageHandler(IEmailSender emailSender) : IMessageHandler
{
    public MessageType MessageType => MessageType.RentStatusChange;

    public async Task SendMessage(RentRecord message)
    {
        await emailSender.SendEmail(new MailRequest
        {
            ToEmail = message.Tenant.Email,
            Body = await emailSender.GetEmailBody(message, RentTemplateType.RentalStatusChange)
        });
    }
}
