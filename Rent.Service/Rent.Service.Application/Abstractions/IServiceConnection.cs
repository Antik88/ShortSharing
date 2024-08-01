namespace Rent.Service.Application.Abstractions;

public interface IServiceConnection
{
    public Task<T> GetFromServiceById<T>(Guid id, string serviceUrl, CancellationToken cancellationToken);
}
