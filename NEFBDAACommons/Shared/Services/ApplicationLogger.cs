using Microsoft.Extensions.Logging;
using NEFBDAACommons.Shared.Extensions;
using System;

namespace NEFBDAACommons.Shared.Services
{
  public static class ApplicationLogger
  {
    static ILoggerFactory _loggerFactory;

    public static ILoggerFactory GetLoggedFactory()
    {
      return _loggerFactory;
    }
    public static void ConfigureLogger(ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory;
    }

    public static ILogger CreateLogger<T>()
    {
      if (_loggerFactory == null)
      {
        throw new InvalidOperationException($"{nameof(ILogger)} is not configured. {nameof(ConfigureLogger)} must be called before use");
      }
      return _loggerFactory.CreateLogger<T>();
    }
    public static ILogger CreateLogger(Type type)
    {
      if (_loggerFactory == null)
      {
        throw new InvalidOperationException($"{nameof(ILogger)} is not configured. {nameof(ConfigureLogger)} must be called before use");
      }

      return _loggerFactory.CreateLogger(type);
    }

    public static void LogDebug(object source, string message)
    {
      CreateLogger(GetTypeForSource(source)).LogDebug(message);
    }



    public static void LogError(object source, string message, Exception ex)
    {
      CreateLogger(GetTypeForSource(source)).LogError(message + "\n" + "Exception details:" + ex.ToLogString());
    }
    private static Type GetTypeForSource(object source)
    {
      if (source is Type)
      {
        return source as Type;
      }
      else
      {
        return source?.GetType();
      }
    }


  }
}
