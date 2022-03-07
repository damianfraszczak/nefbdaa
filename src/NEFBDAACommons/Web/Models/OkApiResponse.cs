namespace NEFBDAACommons.Web.Models
{
    public class OkApiResponse<T> : ApiResponse<T>
    {
        public OkApiResponse() : base(true)
        {

        }
        public OkApiResponse(T content) : base(true, content)
        {

        }
    }
}
