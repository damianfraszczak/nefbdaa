using Microsoft.IdentityModel.Tokens;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NEFBDAACommons.Security.Models.JwtToken
{
  public class JwtToken
  {
    public static string NAME = "name";
    public static string SURNAME = "surname";
    public static string GUID = "guid";
    public static string ROLE = "role";
    public static string ROLES = "roles";
    public static TokenValidationParameters TokenValidationParameters => JwtTokenSettings.TokenValidationParameters;


    public static Dictionary<string, object> Decode(string token)
    {
      return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
    }

    public static string Encode(string sub, string name, string surname, IEnumerable<string> roles, Guid? guid, Dictionary<string, string> additionalParams)
    {
      return new JwtSecurityTokenHandler().WriteToken(CreateJwtSecurityToken(GetClaims(sub, name, surname, roles, guid, additionalParams)));
    }
    public static List<Claim> GetClaims(AuthenticatedUserViewModel user, Dictionary<string, string> additionalParams)
    {
      return GetClaims(user.Id + "", user.FirstName, user.LastName, user.Roles, user.Guid, additionalParams);
    }
    public static List<Claim> GetClaims(string sub, string name, string surname, IEnumerable<string> roles, Guid? guid, Dictionary<string, string> additionalParams)
    {
      var claims = new List<Claim>();
      claims.AddJti();
      claims.AddSub(sub);
      claims.AddName(name);
      claims.AddSurname(surname);
      claims.AddGuid(guid);
      claims.AddRoles(roles);
      claims.AddAdditionalParams(additionalParams);
      return claims;
    }
    private static JwtSecurityToken CreateJwtSecurityToken(IEnumerable<Claim> claims)
    {
      return new JwtSecurityToken
      (
          JwtTokenSettings.Issuer,
          JwtTokenSettings.Audience,
          claims,
          DateTime.UtcNow,
          JwtTokenSettings.Expires,
          JwtTokenSettings.SigningCredentials
      );
    }
  }
}
