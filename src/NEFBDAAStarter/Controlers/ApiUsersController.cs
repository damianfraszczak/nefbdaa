using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Web.Controlers;
using NEFBDAACommons.Web.Models;
using NEFBDAAStarter.Models.ViewModels;


namespace NEFBDAAStarter.Controlers
{
  public class ApiUsersController : AbstractUsersController<AppUserViewModel>
  {
    public ApiUsersController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {
    }
    public override ApiResponse<AppUserViewModel> GetMyAccount()
    {
      return base.GetMyAccount();
    }
  }
}
