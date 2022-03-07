namespace NEFBDAACommons.Database.Models
{
    public enum ConfigEntityType
    {
        STRING,
        INTEGER,
        DOUBLE,
        TEXT,
        LONG_TEXT,
        HTML,
        DOCUMENTS,
    }
    public class ConfigEntity : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }
        public string AppKey { get; set; }
        public ConfigEntityType Type { get; set; }
        public string Value { get; set; }
        public bool HiddenConfig { get; set; }
    }
}
