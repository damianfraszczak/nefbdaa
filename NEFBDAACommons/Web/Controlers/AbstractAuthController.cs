using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Security.Services;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Web.Controlers
{
    public abstract class AbstractAuthController<T> : ApiBaseController where T : BaseUserViewModel
    {
        protected IAuthenticationService AuthenticationService { get; }
        public AbstractAuthController(
            IMemoryCache cache,
            IAuthenticationService authenticationService) : base(cache)
        {
            AuthenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual ApiResponse<string> Authenticate([FromBody] AuthenticateUserViewModel authenticationRequest) => OkResponse(AuthenticationService.Authenticate(authenticationRequest));

        [AllowAnonymous]
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual ApiResponse<string> Register([FromBody] RegisterUserViewModel newUser) => OkResponse(AuthenticationService.Register(newUser));

        [AllowAnonymous]
        [HttpDelete]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual OkApiResponse<object> Logout()
        {
            AuthenticationService.Logout(AuthenticatedUser);
            return OkResponse();
        }
        [AllowAnonymous]
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual OkApiResponse<object> ResetPassword([FromBody]ResetPasswordViewModel resetPassword)
        {
            AuthenticationService.ResetPassword(resetPassword.Email);
            return OkResponse();
        }

       
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual OkApiResponse<object> ChangePassword([FromBody]ChangePasswordViewModel changePassword)
        {
            AuthenticationService.ChangePassword(changePassword,AuthenticatedUser);
            return OkResponse();
        }
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public virtual ApiResponse<string> LoginAsOtherUser(long userId) => OkResponse(AuthenticationService.LoginAsOtherUser(userId, AuthenticatedUser));

    }
}
