using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using ShortSharing.BLL.Services;
using Xunit;

namespace ShortSharing.Tests.CacheTests
{
    public class CacheTest
    {
        private readonly IDistributedCache _cache;
        private readonly CacheService _cacheService;

        public CacheTest()
        {
            _cache = Substitute.For<IDistributedCache>();
            _cacheService = new CacheService(_cache);
        }

        [Fact]
        public async Task GetData_ReturnsDefault_WhenCacheIsEmpty()
        {
            // Arrange
            var key = "nonexistent-key";
            _cache.GetAsync(key).Returns((byte[]?)null);

            // Act
            var result = await _cacheService.GetData<string>(key);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetData_ReturnsDeserializedData_WhenCacheHasData()
        {
            // Arrange
            var key = "existing-key";
            var expectedData = "cached data";
            var serializedData = JsonSerializer.SerializeToUtf8Bytes(expectedData);

            _cache.GetAsync(key).Returns(serializedData);

            // Act
            var result = await _cacheService.GetData<string>(key);

            // Assert
            Assert.Equal(expectedData, result);
        }

        [Theory]
        [InlineData("testKey", "testData")]
        public async Task SetData_ShouldStoreDataInCache(string key, string data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            await _cache.SetStringAsync(key, jsonData, options);
        }

        [Fact]
        public async Task SetData_CallsSetAsyncWithCorrectParameters()
        {
            // Arrange
            var key = "test-key";
            var data = "test data";
            var expectedData = JsonSerializer.SerializeToUtf8Bytes(data);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            // Act
            await _cacheService.SetData(key, data);

            // Assert
            await _cache.Received(1).SetAsync(
                key,
                Arg.Is<byte[]>(bytes => bytes.SequenceEqual(expectedData)),
                Arg.Is<DistributedCacheEntryOptions>(opt =>
                    opt.AbsoluteExpirationRelativeToNow == options.AbsoluteExpirationRelativeToNow
                ));
        }
    }
}
