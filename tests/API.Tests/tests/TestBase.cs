using API.Tests.helpers;
using RestSharp;

namespace API.Tests;

public class TestBase : IDisposable
{
    protected RestClient Client { get; private set; }

    public TestBase()
    {
        Client = CreateClient();
    }

    private RestClient CreateClient()
    {
        var options = new RestClientOptions("https://reqres.in/api")
        {
        };
        var client = new RestClient(options);
        client.AddDefaultHeader("x-api-key", Configuration.ApiKey!);
        return client;
    }
    public void Dispose()
    {
        Client?.Dispose();
    }
}