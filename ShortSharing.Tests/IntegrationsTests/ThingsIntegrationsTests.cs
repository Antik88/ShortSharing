using Xunit;
using AutoMapper;
using NSubstitute;
using ShortSharing.BLL.Models;
using ShortSharing.API.Mappers;
using ShortSharing.BLL.Abstractions;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.Tests.IntegrationsTests;
using ShortSharing.API.Controllers.ThingsController;
using ShortSharing.Tests.Constants;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace ShortSharing.Tests.IntegrationTests;

public class ThingsIntegrationTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    public ThingsIntegrationTests(IntegrationTestWebAppFactory factory)
    {
        _client = factory.CreateClient();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApiProfile());
        });

        _mapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task GetAll_ShouldReturn_ListOfThings()
    {
        var response = await _client.GetAsync(ApiUrls.GetAll, CancellationToken.None);
        var responseData = await response.Content.ReadFromJsonAsync<List<ThingDto>>();

        _mapper.Map<List<ThingDto>>(responseData);

        Assert.NotNull(responseData);
        Assert.IsType<List<ThingDto>>(responseData);
    }

    [Theory, AutoMoqData]
    public async Task GetById_ShouldReturn_CorrectThing(ThingModel mockThing)
    {
        var mockService = Substitute.For<IThingsService>();

        var controller = new ThingController(mockService, _mapper);

        mockService.GetByIdAsync(mockThing.Id, Arg.Any<CancellationToken>()).Returns(mockThing);

        var response = await controller.GetByIdAsync(mockThing.Id, CancellationToken.None);

        _mapper.Map<ThingDto>(response);

        Assert.NotNull(response);
        Assert.IsType<ThingDto>(response);
        Assert.Equal(mockThing.Name, response.Name);
    }

    [Theory, AutoMoqData]
    public async Task GetById_ShouldReturn_NoContent(Guid id)
    {
        var response = await _client.GetAsync($"{ApiUrls.ById}/{id}", CancellationToken.None);

        Assert.Null(response.Content.Headers.ContentType);
    }

    [Theory, AutoMoqData]
    public async Task CreateThing_BadRequest(ThingDto thingDto)
    {
        var response = await _client.PostAsJsonAsync("api/Thing", thingDto);

        Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
    }

    [Theory, AutoMoqData]
    public async Task DeleteAsync_NonExistentId_ShouldReturn_NotFound(Guid thingId)
    {
        // Act
        var response = await _client.DeleteAsync($"{ApiUrls.ById}/{thingId}", CancellationToken.None);

        // Assert
        Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
    }
}
