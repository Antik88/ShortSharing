using Email.Service.Shared;
using MassTransit;
using SharingMessages;

namespace Email.Service.Interfaces;

public interface IEmailSender
{
    Task SendEmail(MailRequest mailrequest);
    Task<string> GetEmailBody(ConsumeContext<RentRecord> context);
}
