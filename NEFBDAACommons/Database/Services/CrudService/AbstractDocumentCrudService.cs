using Microsoft.AspNetCore.Http;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Database.Services.CrudService
{
  public interface IDocumentService : ICrudService<DocumentViewModel>
  {
    IEnumerable<DocumentViewModel> CopyDocumentsForNewOwner(Guid guid, IEnumerable<DocumentViewModel> documents, AuthenticatedUserViewModel authenticatedUser);
    IEnumerable<DocumentViewModel> GetDocumentsForOwner(Guid guid, AuthenticatedUserViewModel authenticatedUser);
    IEnumerable<DocumentViewModel> GetDocumentsForOwnerAndType(Guid guid, string type, AuthenticatedUserViewModel authenticatedUser);

    DocumentViewModel SaveUploadedFile(IFormFile file, Guid ownerGuid, string ownerDocumentType, AuthenticatedUserViewModel authenticatedUser);
    DocumentViewModel CreateImageFromBase64(string base64, AuthenticatedUserViewModel authenticatedUser, string filenameWithExtension = "");
    DocumentViewModel CreateFile(string filename, string content, AuthenticatedUserViewModel authenticatedUser);
    DocumentViewModel CreateFile(IEnumerable<string> dirs, string filename, string content, AuthenticatedUserViewModel authenticatedUser);
    DocumentViewModel CreateFile(IEnumerable<string> dirs, string filename, string content, Guid ownerGuid, AuthenticatedUserViewModel authenticatedUser);
    DocumentViewModel GetToDownload(long id, AuthenticatedUserViewModel authenticatedUser);
  }

  public abstract class AbstractDocumentCrudService : AbstractCrudService<DocumentEntity, DocumentViewModel, IGenericRepository<DocumentEntity>>, IDocumentService
  {
    public AbstractDocumentCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
    {
    }
    public DocumentViewModel CreateImageFromBase64(string base64, AuthenticatedUserViewModel authenticatedUser, string filenameWithExtension = "")
    {
      var base64Image = ImageHelper.GetBase64ImageDetails(base64);
      var filename = filenameWithExtension;
      if (string.IsNullOrEmpty(filename))
      {
        filename = "." + base64Image.Extension;
      }
      var viewModel = new DocumentViewModel()
      {
        OriginalFilename = filename,
        FilePath = FileHelper.CreateFile(new string[] { "uploads", "images" }, filename, base64Image.Base64Data),
        OwnerGuid = authenticatedUser.Guid.Value,
        ContentType = FileHelper.GetContentTypeBasedOnFileExtension(filename),
      };
      viewModel.FilePath = FileHelper.ClearFilePath(viewModel.FilePath  );
      return viewModel;
    }
    public DocumentViewModel CreateFile(
        string filename,
        string content,
        AuthenticatedUserViewModel authenticatedUser)
    {
      return CreateFile(new string[] { FileHelper.UploadsDir(), authenticatedUser.Guid.ToString() }, filename, content, authenticatedUser);
    }
    public DocumentViewModel CreateFile(IEnumerable<string> dirs, string filename, string content, AuthenticatedUserViewModel authenticatedUser)
    {
      return CreateFile(dirs, filename, content, authenticatedUser.Guid.Value, authenticatedUser);
    }

    public DocumentViewModel CreateFile(IEnumerable<string> dirs, string filename, string content, Guid ownerGuid, AuthenticatedUserViewModel authenticatedUser)
    {
      var viewModel = new DocumentViewModel()
      {
        OriginalFilename = filename,
        FilePath = FileHelper.CreateFile(dirs, filename, content),
        OwnerGuid = ownerGuid,
        ContentType = FileHelper.GetContentTypeBasedOnFileExtension(filename),
      };
      return this.Add(viewModel, authenticatedUser);
    }
    public virtual IEnumerable<DocumentViewModel> CopyDocumentsForNewOwner(Guid guid, IEnumerable<DocumentViewModel> documents, AuthenticatedUserViewModel authenticatedUser)
    {

      return BatchAdd(documents.Select(x => CopyViewModel(x)), authenticatedUser, true);
    }

    public virtual IEnumerable<DocumentViewModel> GetDocumentsForOwner(Guid guid, AuthenticatedUserViewModel authenticatedUser)
    {
      return _repository
          .GetAll(
              FilterForGetRequest(authenticatedUser).And(x => x.OwnerGuid == guid),
              IncludesForGetListRequestParams(authenticatedUser))
          .Select(MapToViewModel);
    }

    public virtual IEnumerable<DocumentViewModel> GetDocumentsForOwnerAndType(Guid guid, string type, AuthenticatedUserViewModel authenticatedUser)
    {
      return _repository
          .GetAll(
              FilterForGetRequest(authenticatedUser).And(x => x.OwnerGuid == guid && x.OwnerDocumentType == type),
              IncludesForGetListRequestParams(authenticatedUser))
          .Select(MapToViewModel);
    }
    protected override void BeforeAdd(DocumentEntity entity, DocumentViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
    {
      base.BeforeAdd(entity, viewModel, authenticatedUser);
      ClearFile(entity, authenticatedUser);
    }



    protected override void BeforeUpdate(DocumentEntity entity, DocumentViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
    {
      base.BeforeUpdate(entity, viewModel, authenticatedUser);
      ClearFile(entity, authenticatedUser);
    }
    private void ClearFile(DocumentEntity entity, AuthenticatedUserViewModel authenticatedUser)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        entity.Name = entity.OriginalFilename;
      }
      if (string.IsNullOrEmpty(entity.FileSize))
      {
        entity.FileSize = FileHelper.GetFileSizeInMbAsString(entity.FilePath);
      }
      entity.FilePath = FileHelper.ClearFilePath(entity.FilePath);
    }
    public DocumentViewModel SaveUploadedFile(IFormFile file, Guid ownerGuid, string ownerDocumentType, AuthenticatedUserViewModel authenticatedUser)
    {
      var uploadedDocument = FileHelper.UploadAndSaveFile(file, ownerGuid, ownerDocumentType);
      return Add(uploadedDocument, authenticatedUser);
    }

    public DocumentViewModel GetToDownload(long id, AuthenticatedUserViewModel authenticatedUser)
    {
      var document = GetById(id, authenticatedUser);
      document.LastUsedDate = DateTime.Now;
      document.Hits = document.Hits + 1;
      return document;
    }


  }
}
