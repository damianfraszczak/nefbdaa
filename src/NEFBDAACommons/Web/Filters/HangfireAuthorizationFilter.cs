using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using NEFBDAACommons.Security.Models.Auth;
using System;
using System.Runtime.CompilerServices;

namespace NEFBDAACommons.Web.Filters
{
  public interface IHangfireAuthorizationFilterAuthProvider
  {
    bool CanUserViewDashboard(AuthenticateUserViewModel user);
  }
  public class AlwaysAuthorizedHangfireAuthorizationFilterAuthProvider : IHangfireAuthorizationFilterAuthProvider
  {
    public bool CanUserViewDashboard(AuthenticateUserViewModel user)
    {
      return true;
    }
  }

  public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
  {
    private IHangfireAuthorizationFilterAuthProvider _authProvider;
    public HangfireAuthorizationFilter(IHangfireAuthorizationFilterAuthProvider authProvider)
    {
      _authProvider = authProvider;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool Authorize([NotNull] DashboardContext context)
    {
      var httpContext = context.GetHttpContext();

      var header = httpContext.Request.Headers[CommonsConstants.AUTH_HEADER];

      if (string.IsNullOrWhiteSpace(header))
      {
        return SetChallengeResponse(httpContext);
      }

      var authValues = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

      if (!"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
      {
        return SetChallengeResponse(httpContext);
      }

      var parameter = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
      var parts = parameter.Split(':');

      if (parts.Length < 2)
      {
        return SetChallengeResponse(httpContext);
      }

      var username = parts[0];
      var password = parts[1];

      if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
      {
        return SetChallengeResponse(httpContext);
      }

      try
      {
        if (_authProvider.CanUserViewDashboard(new AuthenticateUserViewModel()
        {
          Email = username,
          Phone = username,
          UID = username,
          Password = password
        }))
        {
          return true;
        }

      }
      catch (Exception)
      {
        //if user it not authenticated service throws an exception
      }


      return SetChallengeResponse(httpContext);

    }

    private bool SetChallengeResponse(HttpContext httpContext)
    {
      httpContext.Response.StatusCode = 401;
      httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
      httpContext.Response.WriteAsync("Authentication is required.");
      return false;
    }

  }
}
