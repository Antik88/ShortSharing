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

namespace ShortSharing.Tests;

public class ThingServiceTests
{
    private readonly IThingsService _thingsService;
    private readonly IGenericRepository<ThingEntity> _thingsRepository;
    private readonly IMapper _mapper;

    public ThingServiceTests()
    {
        _thingsRepository = Substitute.For<IGenericRepository<ThingEntity>>();

        _mapper = Substitute.For<IMapper>();

        _thingsService = new ThingsService(_thingsRepository, _mapper);
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
    public async Task CreateAsync_ValidModel_ReturnsCreatedModel(ThingModel thingModel,
        ThingEntity thingEntity)
    {
        // Arrange
        _mapper.Map<ThingEntity>(thingModel).Returns(thingEntity);

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