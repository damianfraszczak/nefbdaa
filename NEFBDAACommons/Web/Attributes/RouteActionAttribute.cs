using Microsoft.AspNetCore.Mvc;
using System;

namespace NEFBDAACommons.Web.Attributes
{

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RouteActionAttribute : RouteAttribute
    {
        public RouteActionAttribute() : base("[action]")
        {
        }
    }
}
