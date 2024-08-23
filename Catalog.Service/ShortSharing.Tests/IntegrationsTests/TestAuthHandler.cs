using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ShortSharing.Tests.IntegrationsTests;

public class TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger, 
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions> (options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(new[]
{
            new Claim(ClaimTypes.NameIdentifier, "testUser"),
        }, "TestScheme");

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
