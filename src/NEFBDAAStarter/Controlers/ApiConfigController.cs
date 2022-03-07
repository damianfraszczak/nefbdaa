using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Controlers;
using NEFBDAACommons.Web.Models;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;
using System.Linq;

namespace NEFBDAAStarter.Controlers
{
    public class ApiConfigController : AbstractConfigController
    {
        public ApiConfigController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork, IEntityFormFieldDefinitionsService entityFormService) : base(cache, unitOfWork, entityFormService)
        {
        }

        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        [ProducesResponseType(typeof(ErrorApiResponse), 500)]
        public ApiResponse<ConfigAppViewModel> Config() => OkResponse(new ConfigAppViewModel()
        {
            Version = "1.0.0",
            Models = EnumHelper.GetAllAsList<ModelTypes>(typeof(ModelTypes))
             .Select(x => new ModelFieldDefinitionViewModel() {
                 FieldDefinitions = EntityFormService.GetModelFieldsDefinitions(x.ToString(), AuthenticatedUser),
                 ModelType = x.ToString()
             })
             .ToList()

        });

    }
}
