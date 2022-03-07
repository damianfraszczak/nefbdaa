using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAAStarter.Models.Entities;
using System.Linq;

namespace NEFBDAAStarter.Helpers
{
    public static class SecurityHelper
    {

        public static bool IsAdmin(AuthenticatedUserViewModel user)
        {
            return IsRole(user, AppUserRoleEnum.Admin);
        }
        public static bool IsRole(AuthenticatedUserViewModel user, AppUserRoleEnum role)
        {
            var roles = user.Roles.Select(x => EnumHelper.GetValueForString<AppUserRoleEnum>(x));
            return roles.Contains(role);
        }


    }
}
