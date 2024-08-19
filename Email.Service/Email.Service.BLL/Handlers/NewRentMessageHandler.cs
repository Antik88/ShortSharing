using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Enums;
using Email.Service.Interfaces;
using Email.Service.Shared;
using SharingMessages;

namespace Email.Service.BLL.Handlers;

public class NewRentMessageHandler(IEmailSender emailSender) : IMessageHandler
{
    public MessageType MessageType => MessageType.NewRent;

    public async Task SendMessage(RentRecord message)
    {
        await emailSender.SendEmail(new MailRequest
        {
            ToEmail = message.Tenant.Email,
            Body = await emailSender.GetEmailBody(message, RentTemplateType.RentalConfirmationTenant)
        });

        await emailSender.SendEmail(new MailRequest
        {
            ToEmail = message.Owner.Email,
            Body = await emailSender.GetEmailBody(message, RentTemplateType.RentalConfirmationOwner)
        });
    }
}
