using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Web.Controlers
{
  public class BulkUpdateParams<TID, TViewModel>
  {
    public IEnumerable<TID> ElementsToUpdate { get; set; }
    public TViewModel UpdateBy { get; set; }
  }

  public class GetWithEditDataParams<TID>
  {
    public TID Id { get; set; }

    public Dictionary<string, string> AdditionalParams { get; set; }
  }
  public abstract class ApiReadWriteController<TID, TViewModel, TService> : ApiReadController<TID, TViewModel, TService>, IApiReadWriteController<TID, TViewModel>
       where TViewModel : class, IEntityWithId<TID>
       where TService : ICrudService<TViewModel>
  {

    public ApiReadWriteController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {

    }


    // POST api/<controller>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> Post([FromBody]TViewModel value) => OkResponse(_service.Add(value, AuthenticatedUser));
    [HttpPost]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<IEnumerable<TViewModel>> BulkCreate([FromBody] IEnumerable<TViewModel> list) => OkResponse(_service.BulkCreate(list, AuthenticatedUser));

    // PUT api/<controller>/5
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> Put([FromBody]TViewModel value) => OkResponse(_service.Update(value, AuthenticatedUser));

    [HttpPost]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> AddOrUpdate([FromBody]TViewModel value) => OkResponse(_service.AddOrUpdate(value, AuthenticatedUser));


    [HttpPut]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<IEnumerable<TViewModel>> BulkUpdate([FromBody]BulkUpdateParams<TID, TViewModel> bulkUpdateParams) => OkResponse(_service.BulkUpdate(bulkUpdateParams.ElementsToUpdate.Select(x => (object)x), bulkUpdateParams.UpdateBy, AuthenticatedUser));


    [HttpPut]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<IEnumerable<TViewModel>> BulkUpdate([FromBody] IEnumerable<TViewModel> list) => OkResponse(_service.BatchUpdate(list, AuthenticatedUser));
    [HttpPut]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<IEnumerable<TViewModel>> BulkAddOrUpdate([FromBody] IEnumerable<TViewModel> list) => OkResponse(_service.BulkAddOrUpdate(list, AuthenticatedUser));
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> Delete(TID id) => OkResponse(_service.Delete(id, AuthenticatedUser));
    [HttpDelete]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> DeleteById(TID id) => OkResponse(_service.Delete(id, AuthenticatedUser));
    [HttpDelete]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<IEnumerable<TViewModel>> BulkDelete(IEnumerable<TID> elementsToDeleteIds) => OkResponse(_service.BatchDelete(elementsToDeleteIds.Select(x => (object)x), AuthenticatedUser));


    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<TViewModel> Delete([FromBody]TViewModel value) => OkResponse(_service.Delete(value.Id, AuthenticatedUser));


    [HttpPost]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    public virtual ApiResponse<FormModel<TViewModel>> GetWithEditData([FromBody]  GetWithEditDataParams<TID> editDataParams) => OkResponse(_service.GetWithEditDataById(editDataParams.Id, editDataParams.AdditionalParams ?? new Dictionary<string, string>(), AuthenticatedUser));


  }
}
