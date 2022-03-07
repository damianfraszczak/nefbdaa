using System;

namespace NEFBDAACommons.Database.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class StoreAsStringAttribute : Attribute
    {
    }
}
