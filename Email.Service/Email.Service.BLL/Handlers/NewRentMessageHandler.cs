﻿using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Enums;
using Email.Service.Interfaces;
using Email.Service.Shared;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Handlers;

public class NewRentMessageHandler(IEmailSender emailSender) : IMessageHandler
{
    public async Task Handle(ConsumeContext<RentRecord> context)
    {
        await emailSender.SendEmail(new MailRequest
        {
            ToEmail = context.Message.Tenant.Email,
            Body = await emailSender.GetEmailBody(context, RentTemplateType.RentalConfirmationTenant)
        });

        await emailSender.SendEmail(new MailRequest
        {
            ToEmail = context.Message.Owner.Email,
            Body = await emailSender.GetEmailBody(context, RentTemplateType.RentalConfirmationOwner)
        });
    }
}
