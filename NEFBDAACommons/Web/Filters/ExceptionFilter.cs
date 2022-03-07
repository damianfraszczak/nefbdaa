using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Shared.Exceptions;
using NEFBDAACommons.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NEFBDAACommons.Web.Filters
{
  public class ExceptionFilter : ExceptionFilterAttribute
  {
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionFilter(ILogger<ExceptionFilter> logger, IWebHostEnvironment env)
    {
      _logger = logger;
      _env = env;
    }


    public override void OnException(ExceptionContext context)
    {
      ErrorApiResponse responseError;
      if (context.Exception is ApiException)
      {
        // handle explicit 'known' API errors
        var ex = context.Exception as ApiException;
        context.Exception = null;
        responseError = new ErrorApiResponse(ex.Message) { Errors = ex.Errors, UiConfig = ex.Config };

        context.HttpContext.Response.StatusCode = ex.StatusCode;

        _logger.LogError($"Application thrown error: {ex.Message}", ex);
      }
      else if (context.Exception is UnauthorizedAccessException)
      {
        responseError = new ErrorApiResponse("Unauthorized Access");
        context.HttpContext.Response.StatusCode = 401;
        _logger.LogError("Unauthorized Access in Controller Filter.");
      }
      else if (context.Exception is ValidationException)
      {
        var ex = context.Exception as ValidationException;
        responseError = new ErrorApiResponse(ex.Message);
        context.HttpContext.Response.StatusCode = 400;
        _logger.LogError($"ValidationException {ex.Message}");
      }
      else if (context.Exception is FluentValidation.ValidationException)
      {
        var ex = context.Exception as FluentValidation.ValidationException;
        responseError = new ErrorApiResponse(ex.Message);
        context.HttpContext.Response.StatusCode = 400;
        _logger.LogError($"ValidationException {ex.Message}");
      }

      else if (context.Exception is BaseException)
      {
        var ex = context.Exception as BaseException;
        responseError = new ErrorApiResponse(ex.Message);
        context.HttpContext.Response.StatusCode = 400;
        _logger.LogError($"BaseException {ex.Message}");
      }


      else
      {
        // Unhandled errors
        string msg;
        string stack;
        if (_env.IsDevelopment())
        {
          msg = context.Exception.GetBaseException().Message;
          stack = context.Exception.StackTrace;
        }
        else
        {
          msg = "An unhandled error occurred.";
          stack = null;
        }

        responseError = new ErrorApiResponse(msg);
        responseError.Detail = stack;

        context.HttpContext.Response.StatusCode = 500;

        // handle logging here
        _logger.LogError(new EventId(0), context.Exception, msg);
      }

      // always return a JSON result
      context.Result = new JsonResult(responseError);

      base.OnException(context);
    }

    private ErrorApiResponse GenerateResponseBasedOnModelState(ModelStateDictionary modelState)
    {
      var errorResponse = new ErrorApiResponse("Validation Failed")
      {
        Errors = modelState.Keys
              .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
              .ToList()
      };
      return errorResponse;
    }
  }
}
