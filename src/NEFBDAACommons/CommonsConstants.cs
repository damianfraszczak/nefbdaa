using NEFBDAACommons.Security.Models.JwtToken;

namespace NEFBDAACommons
{
  public static class CommonsConstants
  {
    public static readonly int STATUS_CODE_OK = 0;
    public static readonly int STATUS_CODE_DYNAMIC_FORM_INVALID_CONFIGURATION = 1;


    public static readonly string SEND_EMAIL_JOB = "Send emails";

    public static string TOKEN_NAME_PARAM = JwtToken.NAME;
    public static string TOKEN_SURNAME_PARAM = JwtToken.SURNAME;
    public static string TOKEN_GUID_PARAM = JwtToken.GUID;
    public static string TOKEN_ROLE_PARAM = JwtToken.ROLE;

    public static readonly string API_DATE_TIME_FORMAT = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
    public static readonly string CORS_ALLOW_POLICY_NAME = "AllowCors";
    public static readonly string UploadsFolder = "uploads";
    public static readonly string[] CORS_ALLOW_METHODS = new string[] { "GET", "PUT", "POST", "DELETE" };
    public static readonly string LOGGING_CONFIG_SECTION_NAME = "Logging";

    public static readonly string APP_NAME = "APP_NAME";
    public static readonly string USER_REGISTRATION_TEMPLATE = "USER_REGISTRATION_TEMPLATE";
    public static readonly string USER_PASSWORD_RESET_TEMPLATE = "USER_PASSWORD_RESET_TEMPLATE";


    public static readonly string CONFIG_EMAIL_PREFIX = "EMAIL";
    public static readonly string CONFIG_EMAIL_SENDGRID_API_KEY = $"{CONFIG_EMAIL_PREFIX}_SENDGRID_API_KEY";
    public static readonly string CONFIG_EMAIL_HOST = $"{CONFIG_EMAIL_PREFIX}_HOST";
    public static readonly string CONFIG_EMAIL_USER = $"{CONFIG_EMAIL_PREFIX}_USER";
    public static readonly string CONFIG_EMAIL_PASSWORD = $"{CONFIG_EMAIL_PREFIX}_PASSWORD";
    public static readonly string CONFIG_EMAIL_PORT = $"{CONFIG_EMAIL_PREFIX}_PORT";
    public static readonly string CONFIG_EMAIL_DEFAULT_FROM = $"{CONFIG_EMAIL_PREFIX}_DEFAULT_FROM";
    public static readonly string SETTINGS_SECURITY_CONFIG = "SecurityConfig";
    public static readonly string DEFAULT_STORE_LIST_AS_STRING_SEPARATOR = ",";
    public static readonly string AUTH_HEADER = "Authorization";

  
  }
}
