using Microsoft.IdentityModel.JsonWebTokens;
using NEFBDAACommons.Security.Models.JwtToken;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NEFBDAACommons.Web.Extensions
{
  public static class ClaimsPrincipalExtensions
  {
    public static long GetAuthenticatedUserId(this ClaimsPrincipal claimsPrincipal)
    {
      return (long.TryParse(claimsPrincipal.GetClaimValueOfNameIdentifier(), out var userId)) ? userId : 0;
    }
    public static string GetClaimValueOfNameIdentifier(this ClaimsPrincipal claimsPrincipal)
    {
      return claimsPrincipal?.GetClaim(ClaimTypes.NameIdentifier)?.Value;
    }
    public static string GetAuthenticatedUserRole(this ClaimsPrincipal claimsPrincipal)
    {
      return claimsPrincipal.GetClaimValueOfRolesIdentifier();
    }
    public static IEnumerable<string> GetAuthenticatedUserRoles(this ClaimsPrincipal claimsPrincipal)
    {
      return ExporterHelper.GetListOfStringsFromString(claimsPrincipal.GetClaimValueOfRolesIdentifier());
    }
    public static Claim GetClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
      return claimsPrincipal?.FindFirst(claimType);
    }
    public static string GetAuthenticatedUserName(this ClaimsPrincipal claimsPrincipal)
    {
      return claimsPrincipal?.GetClaim(JwtToken.NAME)?.Value;
    }
    public static string GetAuthenticatedUserGuid(this ClaimsPrincipal claimsPrincipal)
    {
      return claimsPrincipal?.GetClaim(JwtToken.GUID)?.Value;
    }
    public static string GetAuthenticatedUserSurname(this ClaimsPrincipal claimsPrincipal)
    {
      return claimsPrincipal?.GetClaim(JwtToken.SURNAME)?.Value;
    }


    public static string GetClaimValueOfRolesIdentifier(this ClaimsPrincipal claimsPrincipal)
    {
      var claim = claimsPrincipal?.GetClaim(JwtToken.ROLE);
      if (claim == null)
      {
        claim = claimsPrincipal?.GetClaim(ClaimTypes.Role);
      }
      return claim?.Value;
    }
    public static Dictionary<string, string> GetAll(this ClaimsPrincipal claimsPrincipal)
    {
      var claimsIdentity = claimsPrincipal?.Identity as ClaimsIdentity;
      return claimsIdentity?.Claims.ToDictionary(x => x.Type, y => y.Value);
    }

    public static void AddJti(this ICollection<Claim> claims)
    {
      claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
    }

    public static void AddRoles(this ICollection<Claim> claims, IEnumerable<string> roles)
    {
      var rolesString = ExporterHelper.StoreListAsString(roles, CommonsConstants.DEFAULT_STORE_LIST_AS_STRING_SEPARATOR);
      claims.Add(new Claim(JwtToken.ROLE, rolesString));
    }

    public static void AddSub(this ICollection<Claim> claims, string sub)
    {
      if (string.IsNullOrEmpty(sub))
      {
        return;
      }
      claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));
    }
    public static void AddName(this ICollection<Claim> claims, string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        return;
      }
      claims.Add(new Claim(JwtToken.NAME, name));
    }
    public static void AddSurname(this ICollection<Claim> claims, string surname)
    {
      if (string.IsNullOrEmpty(surname))
      {
        return;
      }
      claims.Add(new Claim(JwtToken.SURNAME, surname));
    }
    public static void AddGuid(this ICollection<Claim> claims, Guid? guid)
    {
      if (guid != null)
      {
        claims.Add(new Claim(JwtToken.GUID, guid.ToString()));
      }

    }
    public static void AddAdditionalParams(this ICollection<Claim> claims, Dictionary<string, string> additionalParams)
    {
      additionalParams.ToList().ForEach(x => claims.Add(new Claim(x.Key, x.Value)));
    }


  }
}
