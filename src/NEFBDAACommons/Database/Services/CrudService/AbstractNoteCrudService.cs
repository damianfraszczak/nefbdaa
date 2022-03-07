using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface INoteService<T> : ICrudService<T> where T : NoteViewModel
    {
        IEnumerable<T> CopyNotesForNewOwner(Guid guid, IEnumerable<T> notes, AuthenticatedUserViewModel authenticatedUser);
        IEnumerable<T> GetNotesForOwner(Guid guid, AuthenticatedUserViewModel authenticatedUser);


    }
    public class AbstractNoteCrudService<M, V> : AbstractCrudService<M, V, IGenericRepository<M>>, INoteService<V>
                where M : NoteEntity, new()
        where V : NoteViewModel, new()
    {
        public IEnumerable<V> CopyNotesForNewOwner(Guid guid, IEnumerable<V> notes, AuthenticatedUserViewModel authenticatedUser)
        {
            return BatchAdd(notes.Select(x => CopyViewModel(x)), authenticatedUser, true);
        }

        public IEnumerable<V> GetNotesForOwner(Guid guid, AuthenticatedUserViewModel authenticatedUser)
        {
            return _repository
                 .GetAll(
                     FilterForGetRequest(authenticatedUser).And(x => x.OwnerGuid == guid),
                     IncludesForGetListRequestParams(authenticatedUser))
                 .Select(MapToViewModel);
        }
    }
}
