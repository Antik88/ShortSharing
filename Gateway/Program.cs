using Gateway.DI;
using Ocelot.Middleware;
using Prometheus;

namespace Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.UseHttpClientMetrics();

        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        builder.Services.AddAuth0Authentication(builder.Configuration);
        builder.Services.AddOcelotConfiguration(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ClientPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });

        var app = builder.Build();

        app.UseMetricServer();
        app.UseHttpMetrics();

        app.UseCors("ClientPolicy");

        app.UseSwagger();
        app.UseSwaggerForOcelotUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();

        app.Run();
    }
}
