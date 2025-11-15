using System.Security.AccessControl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace UI.Tests.helpers;

public static class Configuration
{
    
    public static IConfigurationRoot Config { get; private set; }
    public static string? BaseUrl => Config["BaseUrl"];
    public static List<Credentials>? Credentials => Config.GetSection("Credentials").Get<List<Credentials>>();
    public static void Configure()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("config/credentials.json", optional: false, reloadOnChange: false)
            .Build();
            Console.WriteLine();
    }
}