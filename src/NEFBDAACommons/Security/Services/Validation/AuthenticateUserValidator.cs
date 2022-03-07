using FluentValidation;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Models.Validation;

namespace NEFBDAACommons.Security.Services.Validation
{
  public class AuthenticateUserValidator : BaseValidator<AuthenticateUserViewModel>
  {
    public AuthenticateUserValidator(string message = "Authentication error") : base(message)
    {
      RuleFor(x => x).NotNull();
      // RuleFor(x => x.Email ?? x.Phone ?? x.UID).NotNull().NotEmpty();
      RuleFor(x => x.Password).NotNull().NotEmpty();
    }
  }
}