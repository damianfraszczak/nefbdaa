namespace NEFBDAACommons.Shared.Models.Validation
{
    public static class DefaultRegexExpressions
    {

        public static readonly string Email = @"^([a-z0-9_\.\-]{3,})@([\da-z\.\-]{3,})\.([a-z\.]{2,6})$";

        public static readonly string Login = "^[a-z0-9_-]{10,50}$";

        public static readonly string Password = @"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=]).*$";

    }
}
