using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Web.Controlers
{
  public abstract class AbstractAppLogController<T> : ApiReadWriteController<long, T, IAppLogService<T>>
      where T : AppLogViewModel

  {
    public AbstractAppLogController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {
    }
    [AllowAnonymous]
    public override ApiResponse<T> Post([FromBody] T value)
    {
      return base.Post(value);
    }
  }
}
