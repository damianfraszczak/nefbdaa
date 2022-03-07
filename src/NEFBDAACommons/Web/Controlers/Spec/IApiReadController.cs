using Microsoft.AspNetCore.Mvc;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Models;
using System.Collections.Generic;

namespace NEFBDAACommons.Web.Controlers
{
  public interface IApiReadController<TID, TViewModel> where TViewModel : class, IEntityWithId<TID>
  {
    ApiResponse<DocumentViewModel> Export([FromBody] ExportDataModel exportDataModel);
    ApiResponse<IEnumerable<TViewModel>> Get();
    ApiResponse<PagedList<TViewModel>> GetPaged(PagedListParameters pagingParams);
    ApiResponse<PagedList<TViewModel>> GetPagedWithFilter([FromBody] GetPagingWithFitlerParams queryParams);
    ApiResponse<PagedListWithOptionsResponse<TViewModel>> GetPagedWithOptions(PagedListParameters pagingParams);
    ApiResponse<PagedListWithOptionsResponse<TViewModel>> GetPagedWithOptionsAndFilter([FromBody] GetPagingWithFitlerParams queryParams);
    ApiResponse<IEnumerable<TViewModel>> GetWithFilter([FromBody] FilterRule rule);
    ApiResponse<ListWithOptionsResponse<TViewModel>> GetWithOptions();
    ApiResponse<ListWithOptionsResponse<TViewModel>> GetWithOptionsAndFilter([FromBody] FilterRule rule);
  }
}