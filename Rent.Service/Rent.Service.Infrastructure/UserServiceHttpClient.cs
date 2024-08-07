namespace Rent.Service.Infrastructure;

public class UserServiceHttpClient : IUserServiceHttpClient
{
    public HttpClient HttpClient { get; private set; }

    public UserServiceHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}
