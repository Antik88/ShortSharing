using User.Service.API.Constants;
using User.Service.API.DI;

namespace User.Service.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApiDependencies(builder.Configuration);

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(builder =>
            builder.WithOrigins(app.Configuration
                .GetSection(AppSettings.AllowedOrigins)
                .Get<string[]>() ?? [])
        );

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
