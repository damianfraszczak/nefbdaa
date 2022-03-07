using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Web.Controlers
{
  public abstract class AbstractConfigController : ApiReadWriteController<long, ConfigViewModel, IConfigService>
  {

    protected IEntityFormFieldDefinitionsService EntityFormService { get; private set; }
    public AbstractConfigController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork, IEntityFormFieldDefinitionsService entityFormService) : base(cache, unitOfWork)
    {
      this.EntityFormService = entityFormService;
    }

    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual ApiResponse<ConfigViewModel> GetByAppKey(string appKey)
        => OkResponse(_service.GetByAppKey(appKey, AuthenticatedUser));


    // TODO po modelu zaladuj serwis i obiekt i jego formularz
    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual ApiResponse<FormModel<object>> GetEditFormModel(string id,
      string modelType) => OkResponse(EntityFormService.GetEditFormModel(modelType, id, AuthenticatedUser));



  }
}
