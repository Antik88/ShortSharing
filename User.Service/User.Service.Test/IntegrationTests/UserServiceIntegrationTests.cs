using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using User.Service.API.Dtos;
using User.Service.DLL.Context;
using System.Net;
using User.Service.Shared;

namespace User.Service.Test.IntegrationTests;

public class UserServiceIntegrationTests : IAsyncLifetime, IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IntegrationTestWebAppFactory _factory;


    public UserServiceIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        _factory = factory;
    }

    public async Task InitializeAsync()
    {
         Seed.InitializeTestDatabase(_context);
    }

    public Task DisposeAsync()
    {
        _context.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Test_User_Exists()
    {
        // Arrange
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "john.doe@example.com");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("John Doe", user.Name);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            Name = "Diana Green",
            Email = "diana.green@example.com",
        };

        // Act
        var response = await _factory.Client.PostAsJsonAsync("/api/user", createUserDto);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CreateUserDto>();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(createUserDto.Name, result.Name);
    }
}
