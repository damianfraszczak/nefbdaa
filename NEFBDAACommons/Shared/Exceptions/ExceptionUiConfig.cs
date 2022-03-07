namespace NEFBDAACommons.Shared.Exceptions
{
    public enum ExceptionUiType
    {
        NOTIFICATION, DIALOG
    }
    public class ExceptionUiConfig
    {
        public int Timeout { get; set; } = 5000;
        public ExceptionUiType Type { get; set; } = ExceptionUiType.NOTIFICATION;
    }
}
