using System.Net.Http.Json;
using Rent.Service.Application.Common.Exceptions;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Infrastructure.Service;
public class ExternalServiceRequests<THttpClient>
        : IExternalServiceRequests<THttpClient> where THttpClient : IHttpClient
{
    private readonly HttpClient _httpClient;

    public ExternalServiceRequests(THttpClient httpClient)
    {
        _httpClient = httpClient.HttpClient;
    }

    public async Task<T> GetFromServiceById<T>(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(id.ToString(), cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new InvalidRequestException(new List<string> { ValidationMessages.ThingNotFound });

        var result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return result;
    }
}