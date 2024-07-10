using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ShortSharing.Tests.IntegrationsTests;

public abstract class BaseIntegrationTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected BaseIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope(); 

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
    }
}
