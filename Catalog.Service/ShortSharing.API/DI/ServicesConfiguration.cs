using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.API.Dtos.Validators;
using ShortSharing.API.Mappers;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.DI;
using ShortSharing.BLL.Services;
using Swashbuckle.AspNetCore.Filters;

namespace ShortSharing.API.DI;

public static class ServicesConfiguration
{
    public static void AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth0Authentication(configuration);

        services.AddBusinessLogicDependencies(configuration);

        services.AddAutoMapper(typeof(ApiProfile));

        services.AddScoped<IThingsService, ThingsService>();
        services.AddTransient<IValidator<CreateThingDto>, CreateThingDtoValidator>();

        services.ConfigureSwagger();
    }

    private static void AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = configuration["Auth0:Domain"];
            options.Audience = configuration["Auth0:Audience"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = "user/roles"
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Catalog api"
            });

            options.AddSecurityDefinition("oauth2",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Jwt Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
