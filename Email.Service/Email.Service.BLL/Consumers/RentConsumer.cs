using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Enums;
using Email.Service.Interfaces;
using Email.Service.Shared;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Consumers;

public class RentConsumer : IConsumer<RentRecord>
{
    private readonly IRentService _rentService;
    private readonly IEmailSender _emailSender;

    public RentConsumer(IRentService rentService, IEmailSender emailSender)
    {
        _rentService = rentService;
        _emailSender = emailSender;
    }

    public async Task Consume(ConsumeContext<RentRecord> context)
    {
        await SendEmailAsync(context, context.Message.Tenant.Email,
             RentTemplateType.RentalConfirmationTenant);

        await SendEmailAsync(context, context.Message.Owner.Email,
            RentTemplateType.RentalConfirmationOwner);

        await _rentService.CreateRent(context);
    }

    private async Task SendEmailAsync(ConsumeContext<RentRecord> context,
        string toEmail, RentTemplateType type)
    {
        var emailBody = await _emailSender.GetEmailBody(context, type);

        var mailRequest = new MailRequest
        {
            ToEmail = toEmail,
            Body = emailBody
        };

        await _emailSender.SendEmail(mailRequest);
    }
}
