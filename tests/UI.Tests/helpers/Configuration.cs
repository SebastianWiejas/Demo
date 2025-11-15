using System.Security.AccessControl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace UI.Tests.helpers;

public static class Configuration
{
    
    public static string? BaseUrl => Configure()["BaseUrl"];
    public static IEnumerable<Credentials>? Credentials => Configure().GetSection("Credentials").Get<IEnumerable<Credentials>>();
    public static IConfigurationRoot Configure()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("config/credentials.json", optional: false, reloadOnChange: false)
            .Build();
    }
}