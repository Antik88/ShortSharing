using Microsoft.Extensions.Caching.Distributed;
using ShortSharing.BLL.Abstractions;
using System.Text.Json;

namespace ShortSharing.BLL.Services;

public class CacheService(IDistributedCache cache) : ICacheService 
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
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };

        await cache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
    }
}
