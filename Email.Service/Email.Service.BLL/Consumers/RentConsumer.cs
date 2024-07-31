using Email.Service.BLL.Interfaces;
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
        var emailBody = await _emailSender.GetEmailBody(context);

        var mailRequest = new MailRequest
        {
            ToEmail = context.Message.Tenant.Email,
            Subject = "Rental request",
            Body = emailBody
        };

        await _emailSender.SendEmail(mailRequest);

        await _rentService.CreateRent(context);
    }
}
