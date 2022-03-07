using System;

namespace NEFBDAACommons.Shared.Models
{
    public abstract class BaseAuditableViewModel<TID> : BaseViewModel<TID>
    {
        public long CreatorUserId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public long? UpdaterUserId { get; set; }
        public DateTime? UpdatedUtc { get; set; }
    }
}
