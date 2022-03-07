namespace NEFBDAACommons.Security.Models.Auth
{
  public class AuthenticateUserViewModel
  {
    public string Phone { get; set; }
    public string UID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FcmToken { get; set; }
  }
}
