using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;
using System.Collections.Generic;

namespace NEFBDAACommons.Web.Controlers
{
    public class ApiEntityFieldsDefinitionsController : ApiBaseController
    {
        private IEntityFormFieldDefinitionsService _entityFormFieldDefinitionsService;


        public ApiEntityFieldsDefinitionsController(
            IMemoryCache cache,
            IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(cache)
        {
            this._entityFormFieldDefinitionsService = entityFormFieldDefinitionsService;
        }

        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public ApiResponse<IEnumerable<FieldDefinitionViewModel>> GetModelFieldsDefinitions(string modelType) => OkResponse(_entityFormFieldDefinitionsService.GetModelFieldsDefinitions(modelType, AuthenticatedUser));


    }
}
