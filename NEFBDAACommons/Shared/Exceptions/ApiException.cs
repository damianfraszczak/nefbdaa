using NEFBDAACommons.Web.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NEFBDAACommons.Shared.Exceptions
{
  [Serializable]
  public class ApiException : BaseException
  {
    public int StatusCode { get; set; }

    public List<ValidationError> Errors { get; set; }
    // 0 from commons constants it is ok
   
    public ApiException(string message, int statusCode = 200, List<ValidationError> errors = null) :
        base(message)
    {
      StatusCode = statusCode;
      Errors = errors;
    }
    public ApiException(Exception ex, int statusCode = 200) : base(ex.Message)
    {
      StatusCode = statusCode;
    }
    public ApiException(string message, ExceptionUiConfig config, int statusCode = 200) : this(message, statusCode)
    {
      this.Config = config;
    }
  }
}
