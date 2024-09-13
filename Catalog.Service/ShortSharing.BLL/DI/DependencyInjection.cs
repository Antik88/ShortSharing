using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Mappers;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.DI;
using ShortSharing.DAL.Interceptors;

namespace ShortSharing.BLL.DI
{
    public static class DependencyInjection
    {
        public static void AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitiesInterceptor>();

            services.AddDataAccessDependencies(configuration);

            services.AddAutoMapper(typeof(MapperBllProfile).Assembly);

            services.AddScoped<IThingsService, ThingsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITypeService, TypeService>();

            services.AddSingleton<MinioClient>(sp =>
            {
                return (MinioClient)new MinioClient()
                    .WithEndpoint("localhost:9000")
                    .WithCredentials("y2tLAOyzfDsPMmDacRNk", "UXAI8RZY19Ad0cb5b2xRFPRB89GCpQOWIp1b21RS")
                    .Build();
            });

            services.AddScoped<IImageService, ImageService>();
        }
    }
}
