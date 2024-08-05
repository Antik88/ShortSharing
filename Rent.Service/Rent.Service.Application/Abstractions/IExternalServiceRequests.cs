namespace Rent.Service.Application.Abstractions;

public interface IExternalServiceRequests<THttpClient>
{
    public Task<T> GetFromServiceById<T>(Guid id, CancellationToken cancellationToken);
}
