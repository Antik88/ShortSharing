using Rent.Service.Application;

namespace Rent.Service.Infrastructure;

public class CatalogServiceHttpClient : ICatalogServiceHttpClient 
{
    public HttpClient HttpClient { get; private set; }

    public CatalogServiceHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}
