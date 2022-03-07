using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Controlers;

namespace NEFBDAAStarter.Controlers
{
    public class ApiDocumentsController : AbstractDocumentsController
    {
        public ApiDocumentsController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
        {
        }
    }
}
