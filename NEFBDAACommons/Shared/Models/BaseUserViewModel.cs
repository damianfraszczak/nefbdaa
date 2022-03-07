
using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Security.Models.Auth;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NEFBDAACommons.Shared.Models
{
  public class BaseUserViewModel : BasicUserViewModel
  {
    [FormDisplay(PredefinedInputType = InputType.Password, Display = false, DisplayOnUpdateForm = false, Order = 10000, Exportable = false)]
    public string Password { get; set; }

    [FormDisplay(Order = 10001, Required = true)]

    public UserStatusEnum Status { get; set; }
    [JsonIgnore]
    [FormDisplay(DisplayNever = true, Exportable = false)]
    public virtual ICollection<string> FcmTokens { get; set; }
  }

}