using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Security.Services;
using NEFBDAACommons.Web.Controlers;
using NEFBDAAStarter.Models.ViewModels;

namespace NEFBDAAStarter.Controlers
{
    public class ApiAuthenticationController : AbstractAuthController<AppUserViewModel>
    {

        public ApiAuthenticationController(IMemoryCache cache, IAuthenticationService authenticationService) : base(cache, authenticationService)
        {

        }

    }
}
