using Microsoft.Extensions.Logging;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Helpers.Security;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Exceptions;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using System.Collections.Generic;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface IUsersService<TViewModel> : ICrudService<TViewModel> where TViewModel : BaseUserViewModel, new()
    {
        void UpdateFcm(string fcm, AuthenticatedUserViewModel authenticatedUser);
        void ResetPassword(string email);
        TViewModel GetByEmail(string email, AuthenticatedUserViewModel authenticatedUser);
        TViewModel Register(RegisterUserViewModel newUser);
        void ChangePassword(ChangePasswordViewModel changePassword, AuthenticatedUserViewModel authenticatedUser);
    }

    public abstract class
        AbstractUserCrudService<TModel, TViewModel, TRepository> : AbstractCrudService<TModel, TViewModel, TRepository>,
            IUsersService<TViewModel>
        where TRepository : class, IUserRepository<TModel>
        where TModel : BaseUser, new()
        where TViewModel : BaseUserViewModel, new()
    {
        protected IConfigService ConfigService { get; }
        protected IEmailService EmailService { get; }

        public AbstractUserCrudService(
            IDatabaseUnitOfWork uow,
            IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService,
            IConfigService configService,
            IEmailService emailService
        ) : base(uow, entityFormFieldDefinitionsService)
        {
            this.ConfigService = configService;
            this.EmailService = emailService;
        }

        public override TViewModel Add(TViewModel value, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true)
        {
            return Register(value, authenticatedUser, commit);
        }

        public virtual void ResetPassword(string email)
        {
            // TODO
            var user = _repository.GetByEmail(email);
            if (user != null)
            {
                if (UserStatusEnum.Active == user.Status)
                {
                    var newPassword = RandomHelper.RandomPassword();
                    user.Password = CryptoHelper.CreateHash(newPassword);
                    _repository.Update(user);
                    _repository.SaveChanges();
                    NotifyUserAboutPasswordReset(user, newPassword, null);
                }
                else
                {
                    throw new ApiException($"User with email = {email} is not active");
                }
            }
            else
            {
                throw new ApiException($"User with email = {email} not exists");
            }
        }

        public void UpdateFcm(string fcm, AuthenticatedUserViewModel authenticatedUser)
        {
            _repository.ChangeDetectChangesLazyLoading(true);
            var user = _repository.GetById(authenticatedUser.Id);
            user.AddFcmToken(fcm);
            Logger.LogDebug(
                $"Adding fcm {fcm} for user {authenticatedUser.Id} current tokens = {user.FcmTokensString}");
            MakeCommitIfNecessary(true, authenticatedUser);
            _repository.ChangeDetectChangesLazyLoading(false);
        }

        protected virtual void NotifyUserAboutPasswordReset(TModel user, string newPassword,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var config = ConfigService.GetAppConfig<BaseAppConfigViewModel>(authenticatedUser);
            if (!string.IsNullOrEmpty(config.UserPasswordResetTemplate))
            {
                EmailService.Add(new EmailViewModel()
                {
                    From = config.EmailDefaultFrom,
                    To = user.Email,
                    Subject = $" {config.AppName} - Password reset",
                    Template = config.UserPasswordResetTemplate,
                    TemplateParams = new Dictionary<string, string>()
                    {
                        {"Password", newPassword},
                        {"FullName", $"{user.FirstName} {user.LastName}"},
                    }
                }, authenticatedUser);
            }
        }

        public TViewModel Register(RegisterUserViewModel newUser)
        {
            return Register(new TViewModel()
            {
                Email = newUser.Email,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Status = UserStatusEnum.Active
            }, null, true);
        }

        protected virtual TViewModel Register(TViewModel value, AuthenticatedUserViewModel authenticatedUser,
            bool commit)
        {
            var currentUser = _repository.FirstOrDefault(x => x.Email == value.Email).Value;
            if (currentUser != null)
            {
                throw new BaseException($"User with email {value.Email} already exists");
            }
            else
            {
                var entity = MapToModel(value);
                entity.Password = CryptoHelper.CreateHash(value.Password);
                BeforeAdd(entity, value, authenticatedUser);
                _repository.Add(entity);
                _repository.SaveChanges();
                var config = ConfigService.GetAppConfig<BaseAppConfigViewModel>(authenticatedUser);
                if (!string.IsNullOrEmpty(config.UserRegistrationTemplate))
                {
                    EmailService.Add(new EmailViewModel()
                    {
                        From = config.EmailDefaultFrom,
                        To = entity.Email,
                        Subject = $" {config.AppName} - registration confirmation",
                        Template = config.UserRegistrationTemplate,
                        TemplateParams = new Dictionary<string, string>()
                        {
                            {"FullName", $"{entity.FirstName} {entity.LastName}"},
                        }
                    }, authenticatedUser);
                }

                return MapToViewModel(entity);
            }
        }

        protected override TViewModel MapToViewModel(TModel model)
        {
            var result = base.MapToViewModel(model);
            result.Password = null;
            return result;
        }

        protected override TModel Map(TModel originalEntity, TModel newEntity)
        {
            newEntity.Password = originalEntity.Password;
            return base.Map(originalEntity, newEntity);
        }


        public void ChangePassword(ChangePasswordViewModel changePassword, AuthenticatedUserViewModel authenticatedUser)
        {
            TModel user = null;
            if (changePassword.UserId != null && changePassword.UserId > 0)
            {
                if (IsAdmin(authenticatedUser) || changePassword.UserId == authenticatedUser.Id)
                {
                    user = _repository.GetById(changePassword.UserId);
                }
                else
                {
                    throw new ApiException(
                        $"User {authenticatedUser.FullName} is not allowed to change password of others users");
                }
            }
            else
            {
                user = _repository.GetById(authenticatedUser.Id);
            }

            user.Password = CryptoHelper.CreateHash(changePassword.Password);
            _repository.Update(user);
            _repository.SaveChanges();
        }

        protected abstract bool IsAdmin(AuthenticatedUserViewModel authenticatedUser);

        public TViewModel GetByEmail(string email, AuthenticatedUserViewModel authenticatedUser)
        {
            return MapToViewModel(_repository.GetByEmail(email));
        }

        protected override void BeforeAdd(TModel entity, TViewModel viewModel,
            AuthenticatedUserViewModel authenticatedUser)
        {
            base.BeforeAdd(entity, viewModel, authenticatedUser);
            SetDefaultUserParams(entity, viewModel, authenticatedUser);
        }

        protected override void BeforeUpdate(TModel entity, TViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
        {
            base.BeforeUpdate(entity, viewModel, authenticatedUser);
            SetDefaultUserParams(entity, viewModel, authenticatedUser);
        }

        protected  virtual void SetDefaultUserParams(TModel entity, TViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
        {
            if (string.IsNullOrEmpty(entity.UID))
            {
                entity.UID = RandomHelper.RandomUID();
            }

            entity.FullName = $"{entity.FirstName ?? ""} {entity.LastName ?? ""}";
        }
    }
}