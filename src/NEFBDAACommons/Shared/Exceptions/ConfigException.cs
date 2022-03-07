using System;

namespace NEFBDAACommons.Shared.Exceptions
{
  [Serializable]
  public class ConfigException : BaseException
  {
    public ConfigException(string message) : base(message)
    {
    }

    public ConfigException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
