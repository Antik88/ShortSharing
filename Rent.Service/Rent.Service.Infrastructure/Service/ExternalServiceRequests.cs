using System.Net.Http.Json;
using Rent.Service.Application.Common.Exceptions;
using Rent.Service.Application.Common.Constants;
using Rent.Service.Application.Abstractions;
using System.Net.Http;

namespace Rent.Service.Infrastructure.Service;

public class ExternalServiceRequests(IHttpClientFactory httpClientFactory) : IExternalServiceRequests 
{
    public async Task<T> GetFromServiceById<T>(Guid id, string clientName, CancellationToken cancellationToken)
    {
        var httpClient = httpClientFactory.CreateClient(clientName);
        var response = await httpClient.GetAsync(id.ToString(), cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new InvalidRequestException(new List<string> { ValidationMessages.ThingNotFound });

        var result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return result;
    }
}
