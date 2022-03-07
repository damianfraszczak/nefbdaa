using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NEFBDAACommons.Configuration.Models
{
    public class AuthConfig
    {
        public string AuthorizationHeaderName { get; set; } = JwtBearerDefaults.AuthenticationScheme;
        public bool UseDefaultTokenConfig { get; set; } = true;
    }
}
