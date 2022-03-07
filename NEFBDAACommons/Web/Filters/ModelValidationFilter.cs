using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Web.Models;
using System.Linq;

namespace NEFBDAACommons.Web.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                if (filterContext.HttpContext.Request.Method == "GET")
                {
                    var result = new BadRequestResult();
                    filterContext.Result = result;
                }
                else
                {
                    var result = new ContentResult();
                    string content = SerializationHelper.JsonSerialize(GenerateErrorResponse(filterContext.ModelState));
                    result.Content = content;
                    result.ContentType = "application/json";

                    filterContext.HttpContext.Response.StatusCode = 400;
                    filterContext.Result = result;
                }
            }
        }

        private ErrorApiResponse GenerateErrorResponse(ModelStateDictionary modelState)
        {
            return new ErrorApiResponse("Validation Failed")
            {
                Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList()
            };
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
