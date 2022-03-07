using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Services;
using NEFBDAACommons.Web.Extensions;
using NEFBDAACommons.Web.Filters;
using NEFBDAACommons.Web.Models;
using System;

namespace NEFBDAACommons.Web.Controlers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ExceptionFilter))]

    public abstract class ApiBaseController : Controller
    {
        private ILogger _logger;

        private readonly IMemoryCache _cache;
        private AuthenticatedUserViewModel _authenticatedUser;
        public AuthenticatedUserViewModel AuthenticatedUser
        {
            get
            {
                if (_authenticatedUser == null)
                {
                    _authenticatedUser = new AuthenticatedUserViewModel()
                    {
                        Id = User.GetAuthenticatedUserId(),
                        Roles = User.GetAuthenticatedUserRoles(),
                        FirstName = User.GetAuthenticatedUserName(),
                        LastName = User.GetAuthenticatedUserSurname(),
                        Guid = ParsingHelper.GetGuid(User.GetAuthenticatedUserGuid()),
                        AllClaims = User.GetAll()
                    };
                }
                return _authenticatedUser;
            }

        }
        public long LoggedUserId { get; private set; }
        public long LoggedUserBranchId { get; private set; }


        public ApiBaseController(IMemoryCache cache)
        {
            this._cache = cache;
        }

        protected static OkApiResponse<TResult> OkResponse<TResult>(TResult content) => new OkApiResponse<TResult>(content);

        protected static OkApiResponse<object> OkResponse() => new OkApiResponse<object>();

        protected bool GetCache<TItem>(object keyObj, out TItem value) => _cache.TryGetValue(Newtonsoft.Json.JsonConvert.SerializeObject(keyObj), out value);


        protected string SetCache<T>(object keyObj, T data, TimeSpan timeSpan = default(TimeSpan))
        {
            if (timeSpan == default(TimeSpan))
                timeSpan = TimeSpan.FromDays(1);
            this._cache.Set(Newtonsoft.Json.JsonConvert.SerializeObject(keyObj), data, timeSpan);
            return "Ok";
        }

        protected ILogger Logger
        {
            get
            {
                return _logger = _logger ?? ApplicationLogger.CreateLogger(GetType());
            }
        }
    }
}
