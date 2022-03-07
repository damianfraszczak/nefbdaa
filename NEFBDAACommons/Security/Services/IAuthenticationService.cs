using NEFBDAACommons.Security.Models.Auth;

namespace NEFBDAACommons.Security.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(AuthenticateUserViewModel authenticationRequest);
        AuthenticatedUserViewModel AuthenticateAndGetLoggedUser(AuthenticateUserViewModel authenticationRequest);
        void Logout(AuthenticatedUserViewModel authenticatedUser);
        AuthenticatedUserViewModel GetUserForScheduleJob();
        void ResetPassword(string email);
        string Register(RegisterUserViewModel user);
        void ChangePassword(ChangePasswordViewModel changePassword, AuthenticatedUserViewModel authenticatedUser);
        string LoginAsOtherUser(long userId, AuthenticatedUserViewModel authenticatedUser);
    }
}
