using Microsoft.AspNetCore.Http;
using NEFBDAACommons.Shared.Extensions;
using System.Net;
using System.Net.Http;

namespace NEFBDAACommons.Shared.Helpers
{
  public static class HttpHelper
  {
    public static T ReadRemoteJson<T>(string url)
    {
      using (WebClient wc = new WebClient())
      {
        var json = wc.DownloadString(url);
        return json.FromJson<T>();
      }
    }
    
  }
}
