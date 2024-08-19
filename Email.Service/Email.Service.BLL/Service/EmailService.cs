using AutoMapper;
using Email.Service.BLL.Models;
using Email.Service.BLL.Settings;
using Email.Service.DAL.Enums;
using Email.Service.DAL.Repository;
using Email.Service.Interfaces;
using Email.Service.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;
using SharingMessages;

namespace Email.Service.Service;

public class EmailService : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ITemplateRepository _templates;
    private readonly IMapper _mapper;

    public EmailService(IOptions<EmailSettings> options,
        ITemplateRepository templateRepository, IMapper mapper)
    {
        _emailSettings = options.Value;
        _templates = templateRepository;
        _mapper = mapper;
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

    public async Task<string> GetEmailBody(RentRecord message, RentTemplateType templateType)
    {
        var templateDoc = await FetchTemplateAsync(templateType);

        if (templateDoc == null || string.IsNullOrEmpty(templateDoc.Body))
        {
            throw new Exception($"Template '{templateType}' not found or its body is null");
        }

        var templateData = CreateTemplateData(message);
        var renderedBody = RenderTemplate(templateDoc.Body, templateData);

        return renderedBody;
    }

    private async Task<TemplateModel> FetchTemplateAsync(RentTemplateType templateType)
    {
        var result = await _templates.FetchTemplateAsync(templateType);

        return _mapper.Map<TemplateModel>(result);
    }

    private object CreateTemplateData(RentRecord message)
    {
        return new
        {
            UserName = message.Tenant.Name ?? string.Empty,
            OwnerName = message.Owner.Name ?? string.Empty,
            ItemName = message.Thing.Name ?? string.Empty,
            StartDate = message.StartDate.ToString("yyyy-MM-dd"),
            EndDate = message.EndDate.ToString("yyyy-MM-dd"),
            Status = message.Status.ToLower() ?? string.Empty,
        };
    }

    private string RenderTemplate(string templateBody, object templateData)
    {
        var template = Scriban.Template.Parse(templateBody);
        return template.Render(templateData, member => member.Name);
    }
}
