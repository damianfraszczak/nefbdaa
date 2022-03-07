using Microsoft.IdentityModel.Tokens;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Text;

namespace NEFBDAACommons.Security.Models.JwtToken
{
    public class JwtTokenSettings
    {
        private static SecurityConfig CONFIG = ConfigurationHelper
            .GetSection<SecurityConfig>(CommonsConstants.SETTINGS_SECURITY_CONFIG);

        public static string Audience => CONFIG.Audience;

        public static DateTime Expires => DateTime.UtcNow.AddDays(
           CONFIG.ExpiresInDays);

        public static string Issuer => CONFIG.Issuer;

        public static string Key => CONFIG.Key;

        public static SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        public static SigningCredentials SigningCredentials => new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512);

        public static TokenValidationParameters TokenValidationParameters { get; } = new TokenValidationParameters
        {
            IssuerSigningKey = JwtTokenSettings.SecurityKey,
            ValidateActor = CONFIG.ValidateActor,
            ValidateAudience = CONFIG.ValidateAudience,
            ValidateIssuerSigningKey = CONFIG.ValidateIssuerSigningKey,
            ValidateLifetime = CONFIG.ValidateLifetime,
            ValidAudience = CONFIG.Audience,
            ValidIssuer = CONFIG.Issuer
        };
        private static string PrivateKey { get; set; }
    }
}
