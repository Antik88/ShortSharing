namespace ShortSharing.BLL.Abstractions;

public interface ICacheService
{
    Task<T?> GetData<T>(string key);
    Task SetData<T>(string key, T data);
}
