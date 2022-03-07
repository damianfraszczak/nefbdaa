namespace NEFBDAACommons.Security.Models
{
    public class SecurityConfig
    {
        public string Audience { get; set; }

        public int ExpiresInDays { get; set; }

        public string Issuer { get; set; }

        public string Key { get; set; }

        public bool ValidateActor { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
    }
}
