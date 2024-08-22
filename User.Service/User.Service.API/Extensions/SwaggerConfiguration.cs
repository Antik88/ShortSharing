using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace User.Service.API.Extensions;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UserService API"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
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
