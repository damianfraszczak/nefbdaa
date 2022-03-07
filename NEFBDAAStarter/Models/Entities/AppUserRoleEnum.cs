using System.ComponentModel;
using NEFBDAACommons.DynamicForms.Attributes;

namespace NEFBDAAStarter.Models.Entities
{
    public enum AppUserRoleEnum
    {
        [Description("Super admin")]
        Admin, User,
        [EnumExtra(HideFromFilter = true)]
        Coordinator
    }
}
