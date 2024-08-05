using MassTransit;
using Rent.Service.API.Mapping;
using Rent.Service.API.Middleware;
using Rent.Service.Application;
using Rent.Service.Infrastructure;
using Serilog;

namespace Rent.Service.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(ApiProfile));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog((context, configuration) => 
            configuration.WriteTo.Console().MinimumLevel.Information());

        builder.Services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
