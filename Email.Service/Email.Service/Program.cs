using Email.Service.BLL.Consumers;
using Email.Service.BLL.DI;
using Email.Service.Helper;
using MassTransit;

namespace Email.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddBusinessLogicDependencies(builder.Configuration);

        builder.Services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            conf.SetInMemorySagaRepositoryProvider();

            var asb = typeof(RentConsumer).Assembly;

            conf.AddConsumers(asb);
            conf.AddSagaStateMachines(asb);
            conf.AddSagas(asb);
            conf.AddActivities(asb);

            conf.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
