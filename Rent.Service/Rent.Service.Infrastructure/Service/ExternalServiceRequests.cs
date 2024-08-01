using System.Net.Http.Json;
using Rent.Service.Application.Common.Exceptions;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Abstractions;

namespace Rent.Service.Infrastructure.Service;

public class ExternalServiceRequests(IHttpClientFactory httpClientFactory) : IExternalServiceRequests 
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    public async Task<T> GetFromServiceById<T>(Guid id, string serviceUlr, CancellationToken cancellationToken)
    {
        var url = serviceUlr + id;

        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new InvalidRequestException(new List<string> { ValidationMessages.ThingNotFound });

        var result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return result;
    }
}
