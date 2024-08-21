using Gateway.DI;
using Ocelot.Middleware;

namespace Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        builder.Services.AddAuth0Authentication(builder.Configuration);
        builder.Services.AddOcelotConfiguration(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerForOcelotUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();

        app.Run();
    }
}
