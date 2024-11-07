using ShortSharing.BLL.Abstractions;
using NSubstitute;
using Xunit;

namespace ShortSharing.Tests.CacheTests
{
    public class CacheTest
    {
        private readonly ICacheService _cacheService;

        public CacheTest()
        {
            _cacheService = Substitute.For<ICacheService>();
        }

        [Theory]
        [InlineData("testKey", "testData")]
        public async Task GetData_ShouldReturnCorrectData_WhenDataExists(string key, string expectedData)
        {
            _cacheService.GetData<string>(key).Returns(expectedData);

            var result = await _cacheService.GetData<string>(key);

            Assert.Equal(expectedData, result);
        }

        [Theory]
        [InlineData("testKey")]
        public async Task GetData_ShouldReturnNull_WhenNoData(string key)
        {
            _cacheService.GetData<string>(key).Returns((string)null);

            var result = await _cacheService.GetData<string>(key);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("testKey")]
        public async Task GetData_ShouldCallGetDataWithCorrectKey(string key)
        {
            await _cacheService.GetData<string>(key);

            await _cacheService.Received(1).GetData<string>(key);
        }
    }
}
