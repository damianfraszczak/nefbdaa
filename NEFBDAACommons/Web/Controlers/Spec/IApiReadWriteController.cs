using Microsoft.AspNetCore.Mvc;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Web.Models;
using System.Collections.Generic;

namespace NEFBDAACommons.Web.Controlers
{
  public interface IApiReadWriteController<TID, TViewModel> : IApiReadController<TID, TViewModel> where TViewModel : class, IEntityWithId<TID>
  {
    ApiResponse<IEnumerable<TViewModel>> BulkDelete(IEnumerable<TID> elementsToDeleteIds);
    ApiResponse<IEnumerable<TViewModel>> BulkUpdate([FromBody] BulkUpdateParams<TID, TViewModel> bulkUpdateParams);
    ApiResponse<TViewModel> Delete(TID id);
    ApiResponse<TViewModel> Delete([FromBody] TViewModel value);
    ApiResponse<TViewModel> DeleteById(TID id);
    ApiResponse<FormModel<TViewModel>> GetWithEditData([FromBody] GetWithEditDataParams<TID> editDataParams);
    ApiResponse<TViewModel> Post([FromBody] TViewModel value);
    ApiResponse<TViewModel> Put([FromBody] TViewModel value);
  }
}