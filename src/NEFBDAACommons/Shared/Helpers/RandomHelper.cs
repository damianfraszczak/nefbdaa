using System;

using System.Text;

namespace NEFBDAACommons.Shared.Helpers
{
  public static class RandomHelper
  {
    private static SecureRandom random = new SecureRandom();
    public static int RandomNumber(int min, int max)
    {
      return random.Next(min, max);
    }
    public static string RandomNumber(int min, int max, int size)
    {
      var builder = new StringBuilder();
      for (int i = 0; i < size; i++)
      {
        builder.Append(RandomNumber(min, max));
      }
      return builder.ToString();
    }
    public static double RandomDouble(double min, double max, double noise = 0)
    {
      var result = random.NextDouble() * (max - min) + min;
      if (noise != 0)
        result += RandomDouble(-noise, noise);
      return result;
    }
    public static string RandomUID()
    {
      var builder = new StringBuilder();
      for (int i = 0; i < 7; i++)
      {
        builder.Append(RandomNumber(0, 10));
      }
      return builder.ToString();
    }
    public static string RandomImei()
    {
      var builder = new StringBuilder();
      for (int i = 0; i < 14; i++)
      {
        builder.Append(RandomNumber(0, 10));
      }
      return builder.ToString();

    }
    public static string RandomString(int size, bool lowerCase)
    {
      var builder = new StringBuilder();
      char ch;
      for (int i = 0; i < size; i++)
      {
        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        builder.Append(ch);
      }
      if (lowerCase)
        return builder.ToString().ToLower();
      return builder.ToString();
    }
    public static string RandomPassword()
    {
      var builder = new StringBuilder();
      builder.Append(RandomString(4, true));
      builder.Append(RandomNumber(1000, 9999));
      builder.Append(RandomString(2, false));
      return builder.ToString();
    }


  }
}
