using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NEFBDAACommons.Shared.Extensions
{
  public static class StringExtensions
  {

    public static bool EqualsIgnoreCase(this string str, string str2)
    {
      return str.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
    }
    public static bool EqualsAnyIgnoreCase(this string str, params string[] strings)
    {
      return strings.FirstOrDefault(x => str.EqualsIgnoreCase(x)) != null;
    }
    public static string SplitCamelCase(this string str)
    {
      return Regex.Replace(
          Regex.Replace(
              str,
              @"(\P{Ll})(\P{Ll}\p{Ll})",
              "$1 $2"
          ),
          @"(\p{Ll})(\P{Ll})",
          "$1 $2"
      );
    }

    /// <summary>
    /// Pretty-prints a property name.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public static string ToFriendlySpacedString(this string input)
    {
      var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);


      var res = r.Replace(input, " ");

      return res;
    }
    public static string ToCamelCase(this string str)
    {
      var x = str.Replace("_", "");
      if (x.Length == 0) return "Null";
      x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
          m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
      return char.ToLower(x[0]) + x.Substring(1);
    }
    public static string ToTitleCase(this string text)
    {
      var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
      return cultureInfo.TextInfo.ToTitleCase(text.ToLower());
    }
    public static string RemoveIntegers(this string input)
    {
      return Regex.Replace(input, @"[\d-]", string.Empty);
    }

    public static string GetDataFromHtmlCDataAndRemoveHtml(this string text)
    {
      return WebUtility.HtmlDecode(Regex.Replace(text, @"(<!\[CDATA\[)|(\]]>)", string.Empty).RemoveHtml());
    }
    public static string RemoveHtml(this string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        text = "";
      }
      var regex = new Regex("<!--(\n|.)*-->");
      var result = regex.Replace(text, String.Empty);
      regex = new Regex("\\<[^\\>]*\\>");
      return regex.Replace(result, String.Empty); ;
    }

    public static string FormatIndentedJson(this string json)
    {
      dynamic parsedJson = JsonConvert.DeserializeObject(json);
      return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }
    public static Stream ToStream(this string text)
    {
      var byteArray = Encoding.ASCII.GetBytes(text);
      return new MemoryStream(byteArray);

    }
    public static T FromJson<T>(this string val)
    {
      return JsonConvert.DeserializeObject<T>(val);
    }
  }
}
