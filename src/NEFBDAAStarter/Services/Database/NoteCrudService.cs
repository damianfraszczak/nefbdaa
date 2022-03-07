using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.Shared.Models;

namespace NEFBDAAStarter.Services.Database
{
    [ScopeService]
    public class NoteCrudService : AbstractNoteCrudService<NoteEntity, NoteViewModel>
    {
    }
}
