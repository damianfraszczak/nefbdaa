using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Security.Services;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;

namespace NEFBDAAStarter.Services.Auth
{
  [ScopeService]
    public class AuthenticationService : BaseAuthenticationService<AppUser, AppUserViewModel>
    {
        public AuthenticationService(IDatabaseUnitOfWork _databaseUnitOfWork,
            IUsersService<AppUserViewModel> usersService
            ) : base(_databaseUnitOfWork, usersService)
        {
        }

        public override AuthenticatedUserViewModel GetUserForScheduleJob()
        {
            return null;
        }

        protected override void VerifyUserCanLoginAsOtherUser(long userId, AuthenticatedUserViewModel authenticatedUser)
        {
            // TODO ok
        }
    }
}
