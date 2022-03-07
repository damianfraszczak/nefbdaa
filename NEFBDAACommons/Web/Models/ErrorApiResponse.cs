namespace NEFBDAACommons.Web.Models
{
    public class ErrorApiResponse : ApiResponse<object>
    {
        public ErrorApiResponse() : base(false)
        {

        }
        public ErrorApiResponse(string msg) : this()
        {
            Message = msg;
        }
    }
}
