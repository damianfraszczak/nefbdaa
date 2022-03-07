using NEFBDAACommons.Security.Models.Auth;
using System;

namespace NEFBDAACommons.Database.Models
{

    public interface IAuditableByModifyEntity
    {
        long CreatorUserId { get; set; }
        DateTime CreatedUtc { get; set; }
        long? UpdaterUserId { get; set; }
        DateTime? UpdatedUtc { get; set; }
    }
    public interface IAuditableByDeleteEntity
    {
        long? DeleterUserId { get; set; }
        DateTime? DeletedUtc { get; set; }

    }
    public interface IAuditableEntity : IAuditableByModifyEntity, IAuditableByDeleteEntity
    {


    }

    public class BaseAuditableByCreateAndModifyEntity<T> : BaseEntity<T>, IAuditableByModifyEntity
    {
        public long CreatorUserId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public long? UpdaterUserId { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public override void Clear()
        {
            base.Clear();
            CreatorUserId = 0;
            CreatedUtc = DateTime.Now;
            UpdaterUserId = null;
            UpdatedUtc = null;
        }
    }
    public class BaseAuditableEntity<T> : BaseAuditableByCreateAndModifyEntity<T>, IAuditableEntity
    {

        public long? DeleterUserId { get; set; }
        public DateTime? DeletedUtc { get; set; }
        public override void Clear()
        {
            base.Clear();
            DeleterUserId = null;
            DeletedUtc = null;
            IsDeleted = false;

        }
    }
}
