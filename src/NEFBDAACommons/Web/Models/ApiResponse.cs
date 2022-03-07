using NEFBDAACommons.Shared.Exceptions;
using System.Collections.Generic;

namespace NEFBDAACommons.Web.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public ExceptionUiConfig UiConfig { get; set; } = new ExceptionUiConfig();
        public List<ValidationError> Errors { get; set; }
        public T Content { get; set; }



        public ApiResponse()
        {

        }
        public ApiResponse(bool success)
        {
            Success = success;
        }
        public ApiResponse(bool success, T content) : this(success)
        {
            Content = content;
        }
    }
}
