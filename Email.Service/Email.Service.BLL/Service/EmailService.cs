using Email.Service.BLL.Models;
using Email.Service.DAL.Context;
using Email.Service.Helper;
using Email.Service.Interfaces;
using Email.Service.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;
using MongoDB.Driver;
using SharingMessages;

namespace Email.Service.Service;

public class EmailService : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly IMongoCollection<Template> _templates;

    public EmailService(IOptions<EmailSettings> options, MongoDbService mongoDbService)
    {
        _emailSettings = options.Value;
        _templates = mongoDbService.Database.GetCollection<Template>("templates");
    }

    public async Task SendEmail(MailRequest mailrequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_emailSettings.Email);
        email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
        email.Subject = mailrequest.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = mailrequest.Body;
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);

        smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);

        await smtp.SendAsync(email);

        smtp.Disconnect(true);
    }

    public async Task<string> GetEmailBody(ConsumeContext<RentRecord> context)
    {
        var filter = Builders<Template>.Filter.Eq(t => t.Name, "rental_confirmation");

        var templateDoc = await _templates.Find(filter).FirstOrDefaultAsync();

        if (templateDoc == null || string.IsNullOrEmpty(templateDoc.Body))
        {
            throw new Exception("Template 'rental_confirmation' not found or its body is null");
        }

        var body = templateDoc.Body
            .Replace("[UserName]", context.Message.Tenant.Name ?? "Guest")
            .Replace("[ItemName]", context.Message.Thing.Name ?? "Unknown Item")
            .Replace("[StartDate]", context.Message.StartDate.ToString("yyyy-MM-dd"))
            .Replace("[EndDate]", context.Message.EndDate.ToString("yyyy-MM-dd"));

        return body;
    }
}
