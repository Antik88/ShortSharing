using Xunit;
using Shouldly;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.BLL.Models;
using ShortSharing.Shared;
using ShortSharing.Tests.IntegrationsTests;
using AutoFixture.Xunit2;
using ShortSharing.API.Controllers;

namespace ShortSharing.Tests;

public class ThingServiceTests
{
    private readonly IThingsService _thingsService;
    private readonly IGenericRepository<ThingEntity> _thingsRepository;
    private readonly IMapper _mapper;
    private readonly IThingRepository _thingRepository;


    public ThingServiceTests()
    {
        _thingsRepository = Substitute.For<IGenericRepository<ThingEntity>>();
        _thingRepository = Substitute.For<IThingRepository>();

        _mapper = Substitute.For<IMapper>();

        _thingsService = new ThingsService(_thingsRepository, _thingRepository, _mapper);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_InvalidId_ReturnNull(Guid id)
    {
        // Arrange
        _thingsRepository.GetByIdAsync(Arg.Any<Guid>(), default).ReturnsNull();

        // Act
        var model = await _thingsService.GetByIdAsync(id, default);

        // Assert
        model.ShouldBe(null);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ValidId_ReturnsNotNull(ThingEntity thingEntity,
        ThingModel thingModel,
        Guid id)
    {
        // Arrange
        _thingsRepository.GetByIdAsync(id, default).Returns(thingEntity);
        _mapper.Map<ThingModel>(thingEntity).Returns(thingModel);

        // Act
        var model = await _thingsService.GetByIdAsync(id, default);

        // Assert
        model.ShouldNotBeNull();
        model.ShouldBeOfType<ThingModel>();
    }

    [Theory, AutoMoqData]
    public async Task DeleteAsync_ValidId_DeletesSuccessfully(Guid id)
    {
        // Arrange

        // Act
        await _thingsService.DeleteAsync(id, default);

        // Assert
        await _thingsRepository.Received(1).DeleteAsync(id, default);
    }

    [Theory, AutoMoqData]
    public async Task UpdateAsync_ValidId_ReturnsUpdatedModel(ThingEntity thingEntity,
        ThingModel thingModel,
        Guid id)
    {
        //Arrange
        _thingsRepository.UpdateAsync(id, thingEntity, default).Returns(thingEntity);

        _mapper.Map<ThingModel>(thingEntity).Returns(thingModel);
        // Act

        var result = await _thingsService.UpdateAsync(id, thingEntity, default);

        // Assert

        result.ShouldNotBeNull();
        result.ShouldBeOfType<ThingModel>();
    }

    [Theory, AutoMoqData]
    public async Task CreateAsync_ShouldReturnCreatedThingModel(ThingModel entity)
    {
        // Arrange

        // Act
        var response = await _thingsService.CreateAsync(entity, default);

        // Assert

        response.ShouldBeNull();
    }

    [Theory, AutoMoqData]
    public async Task GetAllAsync_ShouldReturnPagedResult_WhenDataIsAvailable(
        List<ThingModel> items)
    {
        var queryParameters = new QueryParameters();

        var result = _thingsService.GetAllAsync(queryParameters, CancellationToken.None);

        Assert.Equal(3, items.Count);
        Assert.True(result.IsCompleted);
    }
}