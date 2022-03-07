using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Web.Controlers
{
    public class AbstractNotesController<T> : ApiReadWriteController<long, T, INoteService<T>>
        where T : NoteViewModel

    {

        public AbstractNotesController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
        {
        }
        [HttpGet]
        [RouteAction]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 401)]
        public virtual ApiResponse<IEnumerable<T>> GetForOwner(Guid guid)
            => OkResponse(_service.GetNotesForOwner(guid, AuthenticatedUser));

    }
}
