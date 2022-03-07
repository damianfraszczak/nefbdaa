using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Controlers;

namespace NEFBDAAStarter.Controlers
{
    public class ApiNotesController : AbstractNotesController<NoteViewModel>
    {
        public ApiNotesController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
        {
        }
    }
}
