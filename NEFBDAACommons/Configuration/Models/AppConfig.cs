using NEFBDAACommons.Database.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NEFBDAACommons.Configuration.Models
{
  public class AppConfig
  {
    public bool UseAllowAllCors { get; set; } = true;
    public bool UseDetailedExceptionOnDev { get; set; } = true;
    public bool UseHsts { get; set; } = true;
    public bool UseResponseCaching { get; set; } = true;
    public bool UseMvc { get; set; } = true;
    public string SettingsName { get; set; } = "appsetings";

    public string AppName { get; set; }
    public AuthConfig AuthConfig { get; set; }
    public HangfireConfig HangfireConfig { get; set; }
    public SwaggerConfig SwaggerConfig { get; set; }
    public SpaConfig SpaConfig { get; set; }
    public JsonSerializerSettings JsonSerializerSettings { get; set; }
    public List<string> IgnoreNotAngularPaths { get; set; } = new List<string>() { "api", "swagger", "hangfire" };
    public string DbConnectionName { get; set; } = "DatabaseContext";
    public string LicenceKey { get; set; }
  }
}
