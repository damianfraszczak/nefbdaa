using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Web.Controlers
{
    public class GetPagingWithFitlerParams
    {
        public FilterRule Rule { get; set; }
        public PagedListParameters PagingParams { get; set; }
    }

    public abstract class ApiReadController<TID, TViewModel, TService> : ApiBaseController,
        IApiReadController<TID, TViewModel> where TViewModel : class, IEntityWithId<TID>
        where TService : ICrudService<TViewModel>
    {
        protected ICrudServiceUnitOfWork _unitOfWork;
        protected TService _service;

        public ApiReadController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache)
        {
            this._unitOfWork = unitOfWork;
            this._service = unitOfWork.Service<TService>();
        }

        // GET: api/<controller>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<IEnumerable<TViewModel>> Get()
            => OkResponse(_service.GetAll(AuthenticatedUser));

        // GET: api/<controller>
        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<IEnumerable<ISelectFieldOptionViewModel>> Search(string term,
            string modelType,
            string fieldName)
            => OkResponse(_service.Search(term, modelType, fieldName, AuthenticatedUser));

        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<IEnumerable<TViewModel>> GetWithFilter([FromBody] FilterRule rule)
            => OkResponse(_service.GetAll(rule, AuthenticatedUser));

        // GET api/<controller>/Paged
        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<PagedList<TViewModel>> GetPaged(PagedListParameters pagingParams)
            => OkResponse(_service.GetPaged(pagingParams, AuthenticatedUser));


        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<PagedList<TViewModel>> GetPagedWithFilter(
            [FromBody] GetPagingWithFitlerParams queryParams)
            => OkResponse(_service.GetPaged(queryParams.Rule, queryParams.PagingParams, AuthenticatedUser));

        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<ListWithOptionsResponse<TViewModel>> GetWithOptions()
            => OkResponse(_service.GetAllWithOptions(AuthenticatedUser));

        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<ListWithOptionsResponse<TViewModel>> GetWithOptionsAndFilter(
            [FromBody] FilterRule rule)
            => OkResponse(_service.GetAllWithOptions(rule, AuthenticatedUser));

        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<IEnumerable<ISelectFieldOptionViewModel>> GetOptionsForEdit(
            [FromBody] OptionsForEdit optionsForEdit)
            => OkResponse(_service.GetOptionsForEdit(optionsForEdit, AuthenticatedUser));

        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<PagedListWithOptionsResponse<TViewModel>> GetPagedWithOptions(
            PagedListParameters pagingParams)
            => OkResponse(_service.GetPagedWithOptions(pagingParams, AuthenticatedUser));

        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<PagedListWithOptionsResponse<TViewModel>> GetPagedWithOptionsAndFilter(
            [FromBody] GetPagingWithFitlerParams queryParams)
            => OkResponse(_service.GetPagedWithOptions(queryParams.Rule, queryParams.PagingParams, AuthenticatedUser));

        // http://www.michalbialecki.com/2018/09/03/generic-export-of-csv-files/
        [HttpPost]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<DocumentViewModel> Export([FromBody] ExportDataModel exportDataModel)
        {
            if (exportDataModel.SelectedElements != null && exportDataModel.SelectedElements.Any())
            {
                var rule = new FilterRule()
                {
                    Condition = "and",
                    Field = "Id",
                    Id = "Id",
                    Input = "NA",
                    Operator = "in",
                    Type = "integer",
                    Value = exportDataModel.SelectedElements,
                };
                if (exportDataModel.Rule == null)
                {
                    exportDataModel.Rule = rule;
                }
                else
                {
                    if (exportDataModel.Rule.Rules == null)
                    {
                        exportDataModel.Rule.Rules = new List<FilterRule>();
                    }

                    exportDataModel.Rule.Rules.Add(rule);
                }
            }

            var dataToExport = _service.GetAll(exportDataModel.Rule, AuthenticatedUser);
            IEnumerable<DisplayFieldDefinitionViewModel> fieldsDefinitions = null;
            if (true == exportDataModel.ExportableFields?.Any())
            {
                var exportableFieldsToLower = exportDataModel.ExportableFields.Select(x => x.ToLower()).ToList();
                fieldsDefinitions = _service.GetAllFieldsDefinitionModel(dataToExport, AuthenticatedUser)
                    .Where(x => exportableFieldsToLower.Contains(x.ParameterName.ToLower()))
                    .OrderBy(x => exportableFieldsToLower.IndexOf(x.ParameterName.ToLower()));
            }
            else
            {
                fieldsDefinitions = _service.GetExportFieldsDefinitionModel(dataToExport, AuthenticatedUser);
            }

            //var stream = new MemoryStream(Encoding.UTF8.GetBytes(ExporterHelper.CreateCSV(dataToExport, fieldsDefinitions)));
            //var result = new FileStreamResult(stream, "text/plain");
            //result.FileDownloadName = "export_" + DateTime.Now + ".csv";
            var content = ExporterHelper.ExportData(
                EnumHelper.GetValueForString<DataExportTypeEnum>(exportDataModel.Type), dataToExport,
                fieldsDefinitions);
            var filename =
                $"export_{_service.GetServiceModelType().Name.Replace("ViewModel", "")}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.csv";
            if (!string.IsNullOrEmpty(exportDataModel.Filename))
            {
                filename = $"{exportDataModel.Filename}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.csv";
            }

            return OkResponse(_unitOfWork.Service<IDocumentService>().CreateFile(filename, content, AuthenticatedUser));
        }
    }
}