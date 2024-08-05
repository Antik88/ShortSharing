using Email.Service.BLL.Models;
using Email.Service.DAL.Context;
using Email.Service.DAL.Enums;
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

    public EmailService(IOptions<EmailSettings> options, DbContext dbContext)
    {
        _emailSettings = options.Value;
        _templates = dbContext.Database.GetCollection<Template>("templates");
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

    public async Task<string> GetEmailBody(ConsumeContext<RentRecord> context, RentTemplateType templateType)
    {
        var templateDoc = await FetchTemplateAsync(templateType);

        if (templateDoc == null || string.IsNullOrEmpty(templateDoc.Body))
        {
            throw new Exception($"Template '{templateType}' not found or its body is null");
        }

        var templateData = CreateTemplateData(context);
        var renderedBody = RenderTemplate(templateDoc.Body, templateData);

        return renderedBody;
    }

    private async Task<Template> FetchTemplateAsync(RentTemplateType templateType)
    {
        var filter = Builders<Template>.Filter.Eq(t => t.Type, templateType);
        return await _templates.Find(filter).FirstOrDefaultAsync();
    }

    private object CreateTemplateData(ConsumeContext<RentRecord> context)
    {
        return new
        {
            UserName = context.Message.Tenant.Name ?? "Guest",
            OwnerName = context.Message.Owner.Name ?? "Owner",
            ItemName = context.Message.Thing.Name ?? "Unknown Item",
            StartDate = context.Message.StartDate.ToString("yyyy-MM-dd"),
            EndDate = context.Message.EndDate.ToString("yyyy-MM-dd")
        };
    }

    private string RenderTemplate(string templateBody, object templateData)
    {
        var template = Scriban.Template.Parse(templateBody);
        return template.Render(templateData, member => member.Name);
    }
}
