using Microsoft.Extensions.Configuration;
namespace API.Tests.helpers;

public static class Configuration
{   
    public static string? BaseUrl => Configure()["BaseUrl"];
    private static IConfigurationRoot Configure()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables("API_TESTS")
            .Build();
    }
}