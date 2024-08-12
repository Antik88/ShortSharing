using Microsoft.AspNetCore.Authentication.JwtBearer;
using User.Service.API.Extensions;
using User.Service.API.Mappers;
using User.Service.BLL.DI;
using Microsoft.IdentityModel.Tokens;

namespace User.Service.API.DI;

public static class ServicesConfiguration
{
    public static void AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth0Authentication(configuration);

        services.AddApplicationDependencies(configuration);

        services.AddAutoMapper(typeof(UserApiProfile));

        services.AddHttpClient();

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
            options.Authority = $"https://{configuration["Auth0:Domain"]}/";
            options.Audience = configuration["Auth0:Audience"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = "user/roles"
            };
        });
    }
}