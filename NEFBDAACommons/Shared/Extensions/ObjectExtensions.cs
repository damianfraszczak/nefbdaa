using Microsoft.Extensions.Logging;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace NEFBDAACommons.Shared.Extensions
{
  public static class ObjectExtensions
  {

    public static object GetPropertyValue(this object obj, string propertyName)
    {
      return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
    }
    public static void SetPropertyValue(this object obj, string propertyName, object value)
    {
      obj.GetType().GetProperty(propertyName).SetValue(obj, value, null);
    }
    public static string ToJson(this object obj)
    {
      return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
      });
    }

    public static object GetEntityIdOrThrowException(this object value, string idFieldName = "Id")
    {
      var id = value?.GetPropertyValue(idFieldName);
      if (id == null)
      {
        throw new Exception($"Entity {value.GetType()} don't have {idFieldName} property");
      }
      return id;
    }
    public static Dictionary<string, object> ToDictionary(this object obj)
    {
      return
          TypeDescriptor.GetProperties(obj)
          .GetEnumerator()
          .ToEnumerable<PropertyDescriptor>()
          .ToDictionary(x => x.Name, y => y.GetValue(obj));
    }
    public static string ToInfoString(this object obj, string separator = ", ")
    {
      return string.Join(separator,
          TypeDescriptor.GetProperties(obj)
          .GetEnumerator()
          .ToEnumerable<PropertyDescriptor>()
          .Select(td => $"{td.Name}:{td.GetValue(obj)}")
          );
    }
    public static byte[] ToBytes(this object obj)
    {
      if (obj == null)
      {
        return default;
      }

      using (var memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize(memoryStream, obj);

        return memoryStream.ToArray().Compress();
      }
    }
    public static void LogDebug(this object obj, string message)
    {
      ApplicationLogger.CreateLogger(obj.GetType()).Log(LogLevel.Debug, message);
    }
    public static void LogInfo(this object obj, string message)
    {
      ApplicationLogger.CreateLogger(obj.GetType()).Log(LogLevel.Information, message);
    }
    public static void LogError(this object obj, string message, Exception ex = null)
    {
      if (ex == null)
      {
        ApplicationLogger.CreateLogger(obj.GetType()).Log(LogLevel.Error, message);
      }
      else
      {
        ApplicationLogger.CreateLogger(obj.GetType()).Log(LogLevel.Error, ex, message);
      }

    }
  }
}
