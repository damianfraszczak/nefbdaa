using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Web.Attributes;
using NEFBDAACommons.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace NEFBDAACommons.Web.Controlers
{
  public abstract class AbstractDocumentsController : ApiReadWriteController<long, DocumentViewModel, IDocumentService>

  {

    public AbstractDocumentsController(IMemoryCache cache, ICrudServiceUnitOfWork unitOfWork) : base(cache, unitOfWork)
    {
    }
    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual ApiResponse<IEnumerable<DocumentViewModel>> GetForOwner(Guid guid)
        => OkResponse(_service.GetDocumentsForOwner(guid, AuthenticatedUser));

    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual ApiResponse<IEnumerable<DocumentViewModel>> GetForOwnerAndType(Guid guid, string type)
        => OkResponse(_service.GetDocumentsForOwnerAndType(guid, type, AuthenticatedUser));


    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 400)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    [ProducesResponseType(typeof(ErrorApiResponse), 500)]
    [HttpPost, DisableRequestSizeLimit]
    public virtual ApiResponse<DocumentViewModel> Upload(IFormFile file, Guid ownerGuid, string ownerDocumentType = "")
    {
      return OkResponse(_service.SaveUploadedFile(file, ownerGuid, ownerDocumentType, AuthenticatedUser));

    }

    [AllowAnonymous]
    [HttpGet]
    [RouteAction]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorApiResponse), 401)]
    public virtual IActionResult Download(long id)
    {
      var document = _service.GetToDownload(id, AuthenticatedUser);
      var stream = new MemoryStream(FileHelper.ReadAsBytes(document));
      var result = new FileStreamResult(stream, FileHelper.GetContentTypeBasedOnFileExtension(document.FilePath))
      {
        FileDownloadName = document.OriginalFilename
      };
      return result;
    }


  }
}
