﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Constants;
using ShortSharing.DAL.Context;
using ShortSharing.DAL.Interceptors;
using ShortSharing.DAL.Repositories;

namespace ShortSharing.DAL.DI
{
    public static class DependencyInjection
    {
        public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IThingRepository), typeof(ThingRepository));
            services.AddScoped(typeof(ITypeRepository), typeof(TypeRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IImageRepository), typeof(ImageRepository));
        }
    }
}
