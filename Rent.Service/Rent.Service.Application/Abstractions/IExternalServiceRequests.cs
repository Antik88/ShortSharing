namespace Rent.Service.Application.Abstractions;

public interface IExternalServiceRequests
{
    public Task<T> GetFromServiceById<T>(Guid id, string serviceUrl, CancellationToken cancellationToken);
}
