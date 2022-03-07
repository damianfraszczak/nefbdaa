using NEFBDAACommons.Web.Filters;

namespace NEFBDAACommons.Configuration.Models
{
    public class HangfireConfig
    {
        public int WorkerCount { get; set; } = 1;
        public string DashboardUrl { get; set; } = "/hangfire";
        public IHangfireAuthorizationFilterAuthProvider AuthProvider { get; set; } = new AlwaysAuthorizedHangfireAuthorizationFilterAuthProvider();
        public string ServerName { get;  set; } = "/HangfireServer";
    }
}
