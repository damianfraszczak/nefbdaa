using System;

namespace NEFBDAACommons.DI.Attributes
{
    

    public abstract class DIAttribute : Attribute
    {

    }

    public class SingletonServiceAttribute : DIAttribute
    {

    }
    public class TransientServiceAttribute : DIAttribute
    {

    }
    public class ScopeServiceAttribute : DIAttribute
    {

    }

    public class RepositoryAttribute : DIAttribute
    {

    }
}
