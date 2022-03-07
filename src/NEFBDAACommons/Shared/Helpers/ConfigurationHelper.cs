using Microsoft.Extensions.Configuration;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetValue(string key, string defaultVal = null)
        {
            return Configuration.BaseStartup.Configuration[key] ?? defaultVal;
        }

        public static T GetSection<T>(string key) where T : class, new()
        {
            var result = new T();
            Configuration.BaseStartup.Configuration
                .GetSection(key)
                .Bind(result);
            return result;
        }

        public static int GetIntValue(string key, int defaultVal = 0)
        {
            return ParsingHelper.GetInt(Configuration.BaseStartup.Configuration[key], defaultVal);
        }
        public static double GetDoubleValue(string key, double defaultVal = 0)
        {
            return ParsingHelper.GetDouble(Configuration.BaseStartup.Configuration[key], defaultVal);
        }
    }
}
