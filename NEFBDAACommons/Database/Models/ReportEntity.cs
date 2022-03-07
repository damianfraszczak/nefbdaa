using NEFBDAACommons.Security.Models.Auth;
using System;

namespace NEFBDAACommons.Database.Models
{
    public class ReportEntity : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        // JSON STORED STRING
        public string Query { get; set; }
        // Service type
        public string ServiceType { get; set; }
        // it is visible for all users
        public bool Global { get; set; }
        // creator
        public Guid OwnerGuid { get; set; }

    }
}
