using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAAStarter.Helpers;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.DynamicForms.Models;

namespace NEFBDAAStarter.Services.Database
{
  [ScopeService]
  public class AppUsersCrudService : AbstractUserCrudService<AppUser, AppUserViewModel, IUserRepository<AppUser>>
  {
    private readonly IDocumentService documentsService;

    public AppUsersCrudService(IDatabaseUnitOfWork uow,
      IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService,
      IConfigService configService,
      IEmailService emailService,
      IDocumentService documentsService) : base(uow, entityFormFieldDefinitionsService, configService, emailService)
    {
      this.documentsService = documentsService;
    }

    protected override bool IsAdmin(AuthenticatedUserViewModel authenticatedUser)
    {
      return SecurityHelper.IsAdmin(authenticatedUser);
    }

    public override FormModel<AppUserViewModel> GetWithEditDataById<TID>(TID id, Dictionary<string, string> additionalParams, AuthenticatedUserViewModel authenticatedUser)
    {
      return base.GetWithEditDataById(id, additionalParams, authenticatedUser);
    }

    protected override IEnumerable<Expression<Func<AppUser, object>>> IncludesForGetListRequest(AuthenticatedUserViewModel authenticatedUser)
    {
      return base.IncludesForGetListRequest(authenticatedUser)
          .Append(x => x.Companies);
    }
    protected override IEnumerable<Expression<Func<AppUser, object>>> IncludesForGetOneRequest(AuthenticatedUserViewModel authenticatedUser)
    {
      return base.IncludesForGetListRequest(authenticatedUser)
           .Append(x => x.Companies).Append(x => x.Language);
    }

    protected override void BeforeAdd(AppUser entity, AppUserViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
    {
      base.BeforeAdd(entity, viewModel, authenticatedUser);
      VerifyImage(entity, authenticatedUser);
    }

    protected override void BeforeUpdate(AppUser entity, AppUserViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
    {
      base.BeforeUpdate(entity, viewModel, authenticatedUser);
      VerifyImage(entity, authenticatedUser);
    }

    private void VerifyImage(AppUser entity, AuthenticatedUserViewModel authenticatedUser)
    {
      if (!string.IsNullOrEmpty(entity.UserImage) && ImageHelper.IsBase64StringImage(entity.UserImage))
      {
        entity.UserImage = documentsService.CreateImageFromBase64(entity.UserImage, authenticatedUser).FilePath;
      }
    }

    protected override IEnumerable<Order> GetDefaultOrder()
    {
      return new List<Order>()
      {
        new Order(){Property = "LanguageId", IsAscending = true},
        new Order(){Property = "DateOfBirth", IsAscending = true},
      };
    }
  }
}
