using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NEFBDAACommons.Configuration.Models;
using System.IO;
 
namespace NEFBDAACommons.Configuration
{
    public static class IWebHostEnvironmentExtensions
    {
        public static IConfiguration Configuration(this IWebHostEnvironment environment, AppConfig config)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{config.SettingsName}.json")
                .AddJsonFile($"{config.SettingsName}.{environment.EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
