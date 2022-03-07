using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Web.Controlers
{
  public abstract class AbstractUsersController<TViewModel> : ApiReadWriteController<long, TViewModel, IUsersService<TViewModel>> where TViewModel : BaseUserViewModel, new()
  {
    public AbstractUsersController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {
    }

    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual ApiResponse<TViewModel> GetMyAccount() => OkResponse(_service.GetById(AuthenticatedUser.Id, AuthenticatedUser));

    [HttpPost]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual OkApiResponse<object> ChangePassword([FromBody]ChangePasswordViewModel changePassword)
    {
      _service.ChangePassword(changePassword, AuthenticatedUser);
      return OkResponse();
    }
    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public ApiResponse<object> UpdateFcm(string fcm)
    {
      _service.UpdateFcm(fcm, AuthenticatedUser);
      return OkResponse();
    }



  }
}
