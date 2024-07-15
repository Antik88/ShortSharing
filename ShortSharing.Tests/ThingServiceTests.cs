using Xunit;
using Shouldly;
using AutoMapper;
using Moq;
using ShortSharing.BLL.Abstractions;
using ShortSharing.BLL.Services;
using ShortSharing.DAL.Abstractions;
using ShortSharing.DAL.Entities;
using ShortSharing.BLL.Models;
using ShortSharing.Shared;

namespace ShortSharing.Tests
{
    public class ThingServiceTests
    {
        private readonly IThingsService _thingsService;
        private readonly Mock<IGenericRepository<ThingEntity>> _thingsRepositoryMock;
        private readonly Mock<IThingRepository> _thingRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ThingServiceTests()
        {
            _thingsRepositoryMock = new Mock<IGenericRepository<ThingEntity>>();
            _thingRepositoryMock = new Mock<IThingRepository>();
            _mapperMock = new Mock<IMapper>();

            _thingsService = new ThingsService(_thingsRepositoryMock.Object, _thingRepositoryMock.Object, _mapperMock.Object);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_InvalidId_ReturnNull(Guid id)
        {
            // Arrange
            _thingsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ThingEntity)null);

            // Act
            var model = await _thingsService.GetByIdAsync(id, default);

            // Assert
            model.ShouldBeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ValidId_ReturnsNotNull(ThingEntity thingEntity, ThingModel thingModel, Guid id)
        {
            // Arrange
            _thingsRepositoryMock.Setup(repo => repo.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(thingEntity);
            _mapperMock.Setup(m => m.Map<ThingModel>(thingEntity)).Returns(thingModel);

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
            _thingsRepositoryMock.Verify(repo => repo.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_ValidId_ReturnsUpdatedModel(ThingEntity thingEntity, ThingModel thingModel, Guid id)
        {
            // Arrange
            _thingsRepositoryMock.Setup(repo => repo.UpdateAsync(id, thingEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(thingEntity);
            _mapperMock.Setup(m => m.Map<ThingModel>(thingEntity)).Returns(thingModel);

            // Act
            var result = await _thingsService.UpdateAsync(id, thingEntity, default);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<ThingModel>();
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ShouldReturnCreatedThingModel(ThingModel entity, ThingEntity thingEntity)
        {
            // Arrange
            _mapperMock.Setup(m => m.Map<ThingEntity>(entity)).Returns(thingEntity);

            _thingsRepositoryMock.Setup(repo => repo.CreateAsync(thingEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(thingEntity);

            _mapperMock.Setup(m => m.Map<ThingModel>(thingEntity)).Returns(entity);

            // Act
            var response = await _thingsService.CreateAsync(entity, default);

            // Assert
            response.ShouldNotBeNull();
            response.ShouldBeOfType<ThingModel>();
        }

        [Theory, AutoMoqData]
        public async Task GetAllAsync_ShouldReturnPagedResult_WhenDataIsAvailable(List<ThingEntity> items)
        {
            // Arrange
            var queryParameters = new QueryParameters();
            var pagedResult = new PagedResult<ThingEntity>(
                items,
                items.Count, 
                queryParameters.PageNumber,
                queryParameters.PageSize);

            _thingRepositoryMock.Setup(repo => repo.GetAllAsync(queryParameters, It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _thingsService.GetAllAsync(queryParameters, CancellationToken.None);

            // Assert
            result.TotalCount.ShouldBe(items.Count);
        }
    }
}
