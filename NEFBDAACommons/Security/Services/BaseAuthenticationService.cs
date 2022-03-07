using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.Helpers.Security;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Security.Models.JwtToken;
using NEFBDAACommons.Security.Services.Validation;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Security.Services
{
  public abstract class BaseAuthenticationService<T, TViewModel> : IAuthenticationService
      where T : BaseUser
      where TViewModel : BaseUserViewModel, new()
  {
    protected IDatabaseUnitOfWork Uow { get; }
    protected IUsersService<TViewModel> UsersService { get; }

    public BaseAuthenticationService(
        IDatabaseUnitOfWork _databaseUnitOfWork,
        IUsersService<TViewModel> usersService)
    {
      this.Uow = _databaseUnitOfWork;
      this.UsersService = usersService;
    }



    public virtual AuthenticatedUserViewModel AuthenticateAndGetLoggedUser(AuthenticateUserViewModel authenticationRequest)
    {
      new AuthenticateUserValidator().ValidateThrow(authenticationRequest);
      CreateHash(authenticationRequest);
      var authenticated = GetAuthenticatedUser(Uow.Repository<IUserRepository<T>>().Authenticate(authenticationRequest));
      new AuthenticatedUserValidator().ValidateThrow(authenticated);
      if (string.IsNullOrEmpty(authenticationRequest.FcmToken))
      {
        UsersService.UpdateFcm(authenticationRequest.FcmToken, authenticated);
      }

      return authenticated;
    }
    public virtual string LoginAsOtherUser(long userId, AuthenticatedUserViewModel authenticatedUser)
    {
      VerifyUserCanLoginAsOtherUser(userId, authenticatedUser);
      var authenticated = GetAuthenticatedUser(Uow.Repository<IUserRepository<T>>().GetById(userId));
      new AuthenticatedUserValidator().ValidateThrow(authenticated);
      return CreateJwt(authenticated);
    }

    protected abstract void VerifyUserCanLoginAsOtherUser(long userId, AuthenticatedUserViewModel authenticatedUser);

    public virtual string Authenticate(AuthenticateUserViewModel authenticationRequest)
    {
      return CreateJwt(AuthenticateAndGetLoggedUser(authenticationRequest));
    }

    public virtual void Logout(AuthenticatedUserViewModel authenticatedUser)
    {
      //TODO, now user token is cleared in webapp only
    }
    protected virtual AuthenticatedUserViewModel GetAuthenticatedUser(T authenticated)
    {
      return authenticated.Map<AuthenticatedUserViewModel>();
    }

    protected virtual Dictionary<string, string> GetAdditionalParams(AuthenticatedUserViewModel authenticatedUser)
    {
      return new Dictionary<string, string>();
    }
    protected string CreateJwt(AuthenticatedUserViewModel authenticatedUser)
    {
      var sub = authenticatedUser.Id + "";
      var guid = authenticatedUser.Guid;
      return JwtToken.Encode(sub, authenticatedUser.FirstName, authenticatedUser.LastName, GetJwtUserRoles(authenticatedUser), guid, GetAdditionalParams(authenticatedUser));
    }

    protected virtual IEnumerable<string> GetJwtUserRoles(AuthenticatedUserViewModel authenticatedUser)
    {
      return authenticatedUser.Roles;
    }

    protected virtual void CreateHash(AuthenticateUserViewModel authentication)
    {
      authentication.Password = CryptoHelper.CreateHash(authentication.Password);
    }


    public abstract AuthenticatedUserViewModel GetUserForScheduleJob();

    public virtual void ResetPassword(string email)
    {
      UsersService.ResetPassword(email);
    }

    public virtual string Register(RegisterUserViewModel newUser)
    {
      var user = UsersService.Register(newUser);
      return Authenticate(new AuthenticateUserViewModel()
      {
        Email = newUser.Email,
        Password = newUser.Password,
        Phone = newUser.Phone,
        UID = ""
      });
    }

    public void ChangePassword(ChangePasswordViewModel changePassword, AuthenticatedUserViewModel authenticatedUser)
    {
      UsersService.ChangePassword(changePassword, authenticatedUser);
    }

  }
}
