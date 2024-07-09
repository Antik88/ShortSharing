using ShortSharing.API.Middlewares;
using ShortSharing.API.Mappers;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.DI;
using ShortSharing.BLL.Services;

namespace ShortSharing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configurations = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddBusinessLogicDependencies(configurations);

            builder.Services.AddAutoMapper(typeof(ApiProfile));

            builder.Services.AddScoped<IThingsService, ThingsService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

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
}