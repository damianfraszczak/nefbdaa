using NEFBDAACommons.Database.Models;

namespace NEFBDAACommons.Security.Models
{
    public class UserStatusEnumeration : Enumeration
    {//https://www.tabsoverspaces.com/233708-using-value-converter-for-custom-encryption-of-field-on-entity-framework-core-2-1

        public static UserStatusEnumeration NONE = new UserStatusEnumeration(0, "None");
        public static UserStatusEnumeration ACTIVE = new UserStatusEnumeration(1, "Active");
        public static UserStatusEnumeration INACTIVE = new UserStatusEnumeration(2, "Inactive");
        public UserStatusEnumeration(int id, string name) : base(id, name)
        {
        }
    }
}
