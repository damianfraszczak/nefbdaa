using System;

namespace NEFBDAACommons.DynamicForms.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class EnumExtraAttribute : Attribute
    {
        public bool HideFromFilter { get; set; } = false;
        public bool HideFromEdit { get; set; } = false;
    }
}