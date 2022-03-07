using NEFBDAACommons.Database.Models;

namespace NEFBDAACommons.Security.Models.Auth
{
    public class UserRoleEnumeration : Enumeration
    {
        public static UserRoleEnumeration SUPER_ADMIN = new UserRoleEnumeration(0, "Super admin");
        public UserRoleEnumeration(int id, string name) : base(id, name)
        {
        }
    }
}
