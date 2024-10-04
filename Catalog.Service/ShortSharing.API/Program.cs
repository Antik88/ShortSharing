using ShortSharing.API.Middlewares;
using ShortSharing.API.DI;
using Prometheus;

namespace ShortSharing.API;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configurations = builder.Configuration;

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.UseHttpClientMetrics();

        builder.Services.AddApiDependencies(configurations);

        var app = builder.Build();

        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseMetricServer();
        app.UseHttpMetrics();

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