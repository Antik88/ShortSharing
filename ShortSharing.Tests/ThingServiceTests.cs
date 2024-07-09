using Xunit;
using Shouldly;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Mappers;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.BLL.Models;

namespace ShortSharing.Tests;

public class ThingServiceTests
{
    private readonly IThingsService _thingsService;
    private readonly IGenericRepository<ThingEntity> _thingsRepository;
    private readonly IMapper _mapper;

    public ThingServiceTests()
    {
        _thingsRepository = Substitute.For<IGenericRepository<ThingEntity>>();

        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperBllProfile());
        }).CreateMapper();

        _thingsService = new ThingsService(_thingsRepository, _mapper);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        _thingsRepository.GetByIdAsync(id, default).ReturnsNull();

        // Act
        var model = await _thingsService.GetByIdAsync(id, default);

        // Assert
        model.ShouldBe(null);
    }

    [Theory, AutoMoqData]
    public async Task GetByIdAsync_ValidId_ReturnsNotNull(ThingEntity thingEntity)
    {
        // Arrange
        var id = Guid.NewGuid();
        thingEntity.Id = id;

       _thingsRepository 
            .GetByIdAsync(id, default)
            .Returns(thingEntity);

        // Act
        var model = await _thingsService.GetByIdAsync(id, default);

        // Assert
        model.ShouldNotBeNull();
        model.ShouldBeOfType(typeof(ThingModel));
        model.Id.ShouldBe(id);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_DeletesSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _thingsService.DeleteAsync(id, default);

        // Assert
        await _thingsRepository.Received(1).DeleteAsync(id, default);
    }

    [Theory, AutoMoqData]
    public async Task UpdateAsync_ValidId_ReturnsUpdatedModel(ThingEntity updatedThingEntity)
    {
        // Arrange
        var id = Guid.NewGuid();

        _thingsRepository
            .UpdateAsync(id, Arg.Any<ThingEntity>(), default)
            .Returns(updatedThingEntity);

        // Act
        var updatedModel = await _thingsService.UpdateAsync(id, updatedThingEntity, default);

        // Assert
        updatedModel.ShouldNotBeNull();
    }

    [Theory, AutoMoqData]
    public async Task CreateAsync_ValidModel_ReturnsCreatedModel(ThingModel thingModel)
    {
        // Arrange
        var thingEntity = _mapper.Map<ThingEntity>(thingModel);

        _thingsRepository
            .CreateAsync(Arg.Any<ThingEntity>(), default)
            .Returns(info =>
            {
                var entityArgument = info.Arg<ThingEntity>();
                entityArgument.Id = Guid.NewGuid();

                return entityArgument;
            });

        // Act
        var createdModel = await _thingsService.CreateAsync(thingModel, default);

        // Assert
        createdModel.ShouldNotBeNull();
    }

    [Theory, AutoMoqData]
    public async Task GetAll_ValidData_ReturnsListOfThingsModels(List<ThingEntity> thingsEntity)
    {
        // Arrange
        _thingsRepository 
            .GetAllAsync(default)
            .Returns(thingsEntity);

        var models = _mapper.Map<List<ThingModel>>(thingsEntity);

        // Act
        var result = await _thingsService.GetAllAsync(default);

        // Assert
        result.ShouldBeEquivalentTo(models);
    }
}