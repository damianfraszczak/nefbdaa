using System;

namespace NEFBDAACommons.Database.Models
{
    public class DocumentEntity : BaseAuditableEntity<long>
    {
        public Guid OwnerGuid { get; set; }
        public string Name { get; set; }
        public string OwnerDocumentType { get; set; }
        public string FilePath { get; set; }
        public string OriginalFilename { get; set; }
       
        public string ContentType { get; set; }
        public string FileSize { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public int? Hits { get; set; }
    }
}
