using NEFBDAACommons.DynamicForms.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NEFBDAACommons.Security.Models.Auth
{
  public class RegisterUserViewModel
  {
    [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.Email)]
    public string Email { get; set; }
    [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.Tel)]
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.Password, Display = false)]
    public string Password { get; set; }
  }
}
