using Email.Service.BLL.Consumers;
using Email.Service.BLL.Handlers;
using Email.Service.BLL.Interfaces;
using Email.Service.BLL.Mappers;
using Email.Service.BLL.Settings;
using Email.Service.DAL.DI;
using Email.Service.Interfaces;
using Email.Service.Service;
using Email.Service.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Email.Service.BLL.DI;

public static class DependencyInjection
{
    public static void AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailSender, EmailService>();

        services.AddScoped<IMessageHandlerStrategy, MessageHandlerStrategy>();

        services.AddScoped<IMessageHandler, NewRentMessageHandler>();
        services.AddScoped<IMessageHandler, StatusChangeMessageHandler>();

        services.AddAutoMapper(typeof(BLLProfile).Assembly);

        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            conf.SetInMemorySagaRepositoryProvider();

            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

            var asb = typeof(RentConsumer).Assembly;

            conf.AddConsumers(asb);
            conf.AddSagaStateMachines(asb);
            conf.AddSagas(asb);
            conf.AddActivities(asb);

            conf.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMQSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.VirtualHost, h =>
                {
                    h.Username(rabbitMQSettings.Username);
                    h.Password(rabbitMQSettings.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        services.AddDataAccessDependencies(configuration);
    }
}
