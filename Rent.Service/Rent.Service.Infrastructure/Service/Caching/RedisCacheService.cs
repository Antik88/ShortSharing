using Microsoft.Extensions.Caching.Distributed;
using Rent.Service.Application.Abstractions;
using System.Text.Json;

namespace Rent.Service.Infrastructure.Service.Caching;

public class RedisCacheService(IDistributedCache cache) : IRedisCacheService 
{
    public async Task<T?> GetData<T>(string key)
    {
        var data = await cache.GetAsync(key);

        return data == null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetData<T>(string key, T data)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        };

        await cache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
    }
}
