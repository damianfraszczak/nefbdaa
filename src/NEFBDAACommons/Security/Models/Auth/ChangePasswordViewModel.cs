using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.DynamicForms.Models;

namespace NEFBDAACommons.Security.Models.Auth
{
    public class ChangePasswordViewModel
    {
        [FormDisplay(PredefinedInputType = InputType.Password)]
        public string Password { get; set; }
        public long? UserId { get; set; }
    }
}
