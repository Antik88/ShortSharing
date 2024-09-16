using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Mappers;
using ShortSharing.BLL.Services;
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

            services.AddSingleton<IMinioClient, MinioClient>(sp =>
            {
                return (MinioClient) new MinioClient()
                    .WithEndpoint("minio.sharing:9000")
                    .WithCredentials("Wt8SaiB7P9RlygOQbysi", "Z53w2vYSIDBzEGdYAKVujVq1wmWngca6rSQc06lp")
                    .Build();
            });

            services.AddScoped<IImageService, ImageService>();
        }
    }
}
