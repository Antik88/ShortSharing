using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace Gateway.DI;

public static class ServicesConfiguration
{
    public static void AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
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

    public static void AddOcelotConfiguration(this IServiceCollection services, IConfigurationManager configuration)
    {
        configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

        services.AddOcelot(configuration)
            .AddCacheManager(x => x.WithDictionaryHandle());
    }
}
