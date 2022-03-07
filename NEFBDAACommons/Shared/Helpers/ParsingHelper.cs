using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Shared.Helpers
{
  public static class ParsingHelper
  {
    public static bool GetBool(object value, bool defaultValue = false)
    {
      if (!string.IsNullOrEmpty(GetNotNullString(value)))
        bool.TryParse(GetNotNullString(value), out defaultValue);
      return defaultValue;
    }
    public static int GetInt(object value, int defaultValue = 0)
    {
      if (!string.IsNullOrEmpty(GetNotNullString(value)))
        int.TryParse(GetNotNullString(value), out defaultValue);
      return defaultValue;
    }
    public static long GetLong(object value, long defaultValue = 0)
    {
      if (!string.IsNullOrEmpty(GetNotNullString(value)))
        long.TryParse(GetNotNullString(value), out defaultValue);
      return defaultValue;
    }
    public static double GetDouble(object value, double defaultValue = 0)
    {
      if (!string.IsNullOrEmpty(GetNotNullString(value)))
        double.TryParse(GetNotNullString(value), out defaultValue);
      return defaultValue;
    }
    public static string GetNotNullString(object value, string defaultValue = "")
    {
      return string.IsNullOrEmpty(value?.ToString()) ? defaultValue : value.ToString().Trim();
    }
    public static Guid? GetGuid(object value, Guid? defaultValue = null)
    {
      try
      {
        return Guid.Parse(GetNotNullString(value));
      }
      catch
      {
        return defaultValue;
      }
    }
    public static T GetNotNull<T>(T val, T defaultValue)
    {
      return string.IsNullOrEmpty(GetNotNullString(val)) ? defaultValue : val;
    }
    public static DateTime? GetDateTimeOrDefault(string value, DateTime? defaultValue = null)
    {
      if (!string.IsNullOrEmpty(value))
      {
        var dateTime = DateTime.MinValue;
        DateTime.TryParse(value, out dateTime);
        if (dateTime == DateTime.MinValue)
        {
          var dateAsInt = ParsingHelper.GetLong(value);
          if (dateAsInt > 0)
          {
            dateTime = new DateTime(dateAsInt);
          }
        }

        return dateTime == DateTime.MinValue ? defaultValue : dateTime;
      }
      return defaultValue;
    }


    public static string GetFirstNotNull(string first, string second)
    {
      return string.IsNullOrEmpty(first) ? second : first;
    }
    public static string GetFirstNotEmptyString(params string[] values)
    {
      return values.FirstOrDefault(x => !string.IsNullOrEmpty(x));
    }


  }
}
