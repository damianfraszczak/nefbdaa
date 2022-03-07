using System;
using Microsoft.AspNetCore.Http;
namespace NEFBDAACommons.Helpers.Web
{
    public static class HttpHelper
    {
        public static string GetBaseUrl(HttpRequest request)
        {
            if (string.IsNullOrEmpty(request.Scheme))
            {
                throw new InvalidOperationException("Missing Scheme");
            }
            if (!request.Host.HasValue)
            {
                throw new InvalidOperationException("Missing Host");
            }
            return request.Scheme + "://" + request.Host;
        }
    }
}
