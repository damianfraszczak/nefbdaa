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
using System.Linq.Expressions;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface IConfigService : ICrudService<ConfigViewModel>
    {
        ConfigViewModel GetByAppKey(string appKey, AuthenticatedUserViewModel authenticatedUser);
        IEnumerable<ConfigViewModel> GetByAppKeyPrefix(string appKeyPrefix, AuthenticatedUserViewModel authenticatedUser);

        T GetAppConfig<T>(AuthenticatedUserViewModel authenticatedUser) where T : BaseAppConfigViewModel, new();
    }

    public class AbstractConfigCrudService : AbstractCrudService<ConfigEntity, ConfigViewModel, IGenericRepository<ConfigEntity>>, IConfigService
    {
        public AbstractConfigCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
        {
        }

        public T GetAppConfig<T>(AuthenticatedUserViewModel authenticatedUser) where T : BaseAppConfigViewModel, new()
        {
            return ReflectionHelper.CreateInstance<T>(typeof(T), GetAll(authenticatedUser));
        }

        public ConfigViewModel GetByAppKey(string appKey, AuthenticatedUserViewModel authenticatedUser)
        {
            return MapToViewModel(_repository.FirstOrDefault(x => x.AppKey == appKey));
        }

        protected override Expression<Func<ConfigEntity, bool>> FilterForGetRequest(AuthenticatedUserViewModel authenticatedUser)
        {
            return base.FilterForGetRequest(authenticatedUser)
                .And(x => x.HiddenConfig != true);
        }

        public IEnumerable<ConfigViewModel> GetByAppKeyPrefix(string appKeyPrefix, AuthenticatedUserViewModel authenticatedUser)
        {
            return _repository
                .GetAll(x => x.AppKey.Contains(appKeyPrefix))
                .Select(x => MapToViewModel(x))
                .ToList();
        }
    }
}
