using System.ComponentModel.DataAnnotations.Schema;

namespace NEFBDAACommons.Database.Models
{
    [Table("AppLogs")]
    public class AppLogEntity : BaseEntity<long>
    {
        public long? CreatorUserId { get; set; }
        public string Level { get; set; }
        public string Timestamp { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
        public string Message { get; set; }
        public string AdditionalData { get; set; }
    }
}
