using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEFBDAACommons.Database.Models
{
    public interface IEntity
    {
        bool IsDeleted { get; set; }
        Guid? Guid { get; set; }

        void Clear();
    }
    public interface IEntityWithIdAsStr
    {
        string IdStr();
    }

    public interface IEntityWithId<TID> : IEntityWithIdAsStr
    {
        TID Id { get; set; }
    }
    public abstract class BaseEntity<TID> : IEntity, IEntityWithId<TID>
    {
        //Id or EntityNameId is automatically used as id
        //and also automatically generated
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TID Id { get; set; }

        [NotMapped]
        private Guid? _guid;
        public Guid? Guid
        {
            get
            {
                if (_guid == null) _guid = System.Guid.NewGuid();
                return _guid;
            }

            set
            {
                _guid = value;
            }
        }
        public bool IsDeleted { get; set; }

        public virtual void Clear()
        {
            Guid = null;
            IsDeleted = false;
        }

        public string IdStr()
        {
            return ParsingHelper.GetNotNullString(Id);
        }

        public override string ToString()
        {
            return this.ToInfoString();
        }
    }
}
