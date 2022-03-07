namespace NEFBDAACommons.Shared.Models
{
    public class AppLogViewModel : BaseViewModel<long>
    {
        public long? CreatorUserId { get; set; }
        public string Level { get; set; }
        public string Timestamp { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
        public string Message { get; set; }
        public object Additional { get; set; }

        public override string OptionText => $"{Level} {Timestamp} {FileName} {LineNumber} {Message}";
    }
}
