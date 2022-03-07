using System;
using System.Collections.Generic;
using System.Linq;
using NEFBDAACommons.Shared.Models;

namespace NEFBDAACommons.Security.Models.Auth
{
  public class AuthenticatedUserViewModel : BaseViewModel<long>
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    // public string Role { get; set; }
    public override string OptionText => $"{FirstName} {LastName}";
    public Dictionary<string, string> AllClaims { get; set; }
    public string GetClaim(string name)
    {
      AllClaims.TryGetValue(name, out string result);
      return result;
    }

    public string FullName { get { return FirstName + " " + LastName; } }

    public bool IsInAnyRole(params string[] roles)
    {
      var lowerRoles = roles.Select(x => x.ToLower());
      return Roles.FirstOrDefault(x => lowerRoles.Contains(x.ToLower())) != null;
    }

    public override string ToString()
    {
      return OptionText;
    }
  }
}
