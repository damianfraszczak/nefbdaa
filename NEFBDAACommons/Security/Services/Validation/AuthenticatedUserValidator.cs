using FluentValidation;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Models.Validation;

namespace NEFBDAACommons.Security.Services.Validation
{
    public class AuthenticatedUserValidator : BaseValidator<AuthenticatedUserViewModel>
    {
        public AuthenticatedUserValidator(string message = "Login details incorrect") : base(message)
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
