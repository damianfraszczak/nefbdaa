using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Web.Controlers;
using NEFBDAAStarter.Models.ViewModels;

namespace NEFBDAAStarter.Controlers
{
  public class ApiCompaniesController : ApiReadWriteController<long, CompanyViewModel, ICrudService<CompanyViewModel>>
  {
    public ApiCompaniesController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {
    }
  }
}
