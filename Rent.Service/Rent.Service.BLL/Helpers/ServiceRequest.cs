using System.Net.Http.Json;

namespace Rent.Service.BLL.Helpers;

public class ServiceRequest(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    public async Task<T> GetModelFromServiceAsync<T>(string connectionString, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(connectionString, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return result;
    }
}
