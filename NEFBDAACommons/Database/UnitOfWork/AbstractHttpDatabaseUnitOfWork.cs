using Microsoft.AspNetCore.Http;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Web.Extensions;
using System;

namespace NEFBDAACommons.Database.UnitOfWork
{
  public class AbstractHttpDatabaseUnitOfWork : AbstractDatabaseUnitOfWork
  {
    private readonly IHttpContextAccessor httpAccessor;

    public AbstractHttpDatabaseUnitOfWork(IHttpContextAccessor httpAccessor) : base()
    {
      this.httpAccessor = httpAccessor;

    }

    protected override AbstractDbContext GetContext(Type repositoryImplementation)
    {
      var context = base.GetContext(repositoryImplementation);
      context.AuthenticatedUser = GetAuthenticatedUser(httpAccessor);
      return context;
    }
    protected virtual AuthenticatedUserViewModel GetAuthenticatedUser(IHttpContextAccessor httpAccessor)
    {
      if (httpAccessor == null) return new AuthenticatedUserViewModel() { };
      var model = new AuthenticatedUserViewModel
      {
        Id = httpAccessor.HttpContext?.User?.GetAuthenticatedUserId() ?? 0,
        Roles = httpAccessor.HttpContext?.User?.GetAuthenticatedUserRoles(),
        Guid = ParsingHelper.GetGuid(httpAccessor.HttpContext?.User?.GetAuthenticatedUserGuid())
      };

      return model;
    }
  }
}
