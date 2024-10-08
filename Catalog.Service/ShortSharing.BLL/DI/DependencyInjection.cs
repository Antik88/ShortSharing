﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Constants;
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

            services.AddTransient<IMinioClient, MinioClient>(sp =>
            {
                return (MinioClient)new MinioClient()
                    .WithEndpoint(BLLConst.MinioUrl)
                    .WithCredentials(configuration[BLLConst.MinioAccessKey], configuration[BLLConst.SecretKey])
                    .Build();
            });

            services.AddScoped<IImageService, ImageService>();
        }
    }
}
