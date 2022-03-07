using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NEFBDAACommons.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace NEFBDAACommons.Database.Extensions
{
    public static partial class DbContextExtensions
    {



        public static IEnumerable<EntityEntry> GetChangedEntities(this DbContext context)
        {
            return context.ChangeTracker.Entries()
                 .Where(e => e.State == EntityState.Added
                          || e.State == EntityState.Modified
                          || e.State == EntityState.Deleted);
        }
        /// <summary>
        /// Populate special properties for all Auditable Entities in context.
        /// </summary>
        public static void UpdateAuditableEntities(this DbContext context, long editorUserId)
        {
            DateTime utcNow = DateTime.UtcNow;

            var changedEntries = context.GetChangedEntities();

            foreach (var dbEntry in changedEntries)
            {
                UpdateAuditableEntity(dbEntry, utcNow, editorUserId);
            }
        }


        private static void UpdateAuditableEntity(
            EntityEntry dbEntry, DateTime utcNow, long editorUserId)
        {
            object entity = dbEntry.Entity;

            switch (dbEntry.State)
            {
                case EntityState.Added:
                    if (entity is IAuditableByModifyEntity creationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        creationAuditable.CreatorUserId = editorUserId;
                    }
                    break;

                case EntityState.Modified:
                    if (entity is IAuditableByModifyEntity modificationAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        modificationAuditable.UpdaterUserId = editorUserId;
                        dbEntry.CurrentValues[nameof(IAuditableByModifyEntity.UpdaterUserId)] = editorUserId;
                        PreventPropertyOverwrite<long>(
                               dbEntry, nameof(IAuditableByModifyEntity.CreatorUserId));

                    }
                    break;

                case EntityState.Deleted:
                    if (entity is IAuditableByDeleteEntity deletionAuditable)
                    {
                        UpdateTrackableEntity(dbEntry, utcNow);
                        // change CurrentValues after dbEntry.State becomes EntityState.Unchanged
                        deletionAuditable.DeleterUserId = editorUserId;
                        dbEntry.CurrentValues[nameof(IAuditableByDeleteEntity.DeleterUserId)] = editorUserId;
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }


        public static void UpdateTrackableEntities(this DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;

            var changedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                         || e.State == EntityState.Modified
                         || e.State == EntityState.Deleted);

            foreach (var dbEntry in changedEntries)
            {
                UpdateTrackableEntity(dbEntry, utcNow);
            }
        }

        private static void UpdateTrackableEntity(EntityEntry dbEntry, DateTime utcNow)
        {
            object entity = dbEntry.Entity;

            switch (dbEntry.State)
            {
                case EntityState.Added:
                    if (entity is IAuditableByModifyEntity creationTrackable)
                    {
                        creationTrackable.CreatedUtc = utcNow;
                    }
                    break;

                case EntityState.Modified:
                    if (entity is IAuditableByModifyEntity modificatonTrackable)
                    {
                        modificatonTrackable.UpdatedUtc = utcNow;
                        dbEntry.CurrentValues[nameof(IAuditableByModifyEntity.UpdatedUtc)] = utcNow;
                        PreventPropertyOverwrite<DateTime>(dbEntry, nameof(IAuditableByModifyEntity.CreatedUtc));

                    }
                    break;

                case EntityState.Deleted:
                    if (entity is IEntity softDeletable)
                    {
                        dbEntry.State = EntityState.Unchanged;
                        softDeletable.IsDeleted = true;
                        dbEntry.CurrentValues[nameof(IEntity.IsDeleted)] = true;

                        if (entity is IAuditableByDeleteEntity deletionTrackable)
                        {
                            deletionTrackable.DeletedUtc = utcNow;
                            dbEntry.CurrentValues[nameof(IAuditableByDeleteEntity.DeletedUtc)] = utcNow;
                        }
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// If we set <see cref="EntityEntry.State"/> to <see cref="EntityState.Modified"/> on entity with
        /// empty <see cref="ICreationTrackable.CreatedUtc"/> or <see cref="ICreationAuditable.CreatorUserId"/>
        /// we should not overwrite database values.
        /// https://github.com/gnaeus/EntityFramework.CommonTools/issues/4
        /// </summary>
        private static void PreventPropertyOverwrite<TProperty>(EntityEntry dbEntry, string propertyName)
        {
            var propertyEntry = dbEntry.Property(propertyName);

            if (propertyEntry.IsModified && Equals(dbEntry.CurrentValues[propertyName], default(TProperty)))
            {
                propertyEntry.IsModified = false;
            }
        }
    }
}
