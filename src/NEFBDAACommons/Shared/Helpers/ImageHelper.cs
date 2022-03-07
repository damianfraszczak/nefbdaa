using System;
using System.Text.RegularExpressions;

namespace NEFBDAACommons.Shared.Helpers
{
  public class Base64ImageDetails
  {
    public byte[] Base64Data { get; set; }
    public string Extension { get; set; }
  }
  public static class ImageHelper
  {


    public static Base64ImageDetails GetBase64ImageDetails(string base64String)
    {
      var type = Regex.Match(base64String, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Replace(";base64", "");
      var base64Data = Regex.Match(base64String, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

      return new Base64ImageDetails() { Base64Data = Convert.FromBase64String(base64Data), Extension = type };
    }
    public static bool IsBase64StringImage(string s)
    {
      return s.StartsWith("data:image");
    }
  }
}
