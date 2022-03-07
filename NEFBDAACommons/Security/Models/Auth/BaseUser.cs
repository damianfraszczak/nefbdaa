using NEFBDAACommons.Database.Models;
using NEFBDAACommons.DynamicForms.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NEFBDAACommons.Security.Models.Auth
{
  public abstract class BaseUser : BaseEntity<long>
  {
   
    public string UID { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    [FormDisplay(Order = int.MaxValue)]
    public UserStatusEnum Status { get; set; }

    public string FcmTokensString { get; set; }


    [NotMapped]
    public virtual IList<string> FcmTokens
    {
      get
      {
        return string.IsNullOrEmpty(FcmTokensString) ? new List<string>() : FcmTokensString.Split(',').ToList();
      }
      set
      {
        FcmTokensString = string.Join(",", value);
      }
    }

    public void AddFcmToken(string fcm)
    {
      var tokens = FcmTokens;
      if (!tokens.Contains(fcm))
      {
        tokens.Add(fcm);
        FcmTokens = tokens;
      }
    }
  }
}
