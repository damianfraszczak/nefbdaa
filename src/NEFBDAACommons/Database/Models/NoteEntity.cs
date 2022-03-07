using System;

namespace NEFBDAACommons.Database.Models
{
    public class NoteEntity : BaseAuditableEntity<long>
    {
        public Guid OwnerGuid { get; set; }
        public string Note { get; set; }

    }
}
