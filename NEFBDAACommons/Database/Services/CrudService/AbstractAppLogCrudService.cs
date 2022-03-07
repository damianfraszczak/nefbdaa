using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Models;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface IAppLogService<T> : ICrudService<T> where T : AppLogViewModel
    {

    }
    public abstract class AbstractAppLogCrudService<M, V> : AbstractCrudService<M, V, IGenericRepository<M>>, IAppLogService<V>
                where M : AppLogEntity, new()
        where V : AppLogViewModel, new()
    {
    protected override void BeforeAdd(M entity, V viewModel, AuthenticatedUserViewModel authenticatedUser)
    {
      base.BeforeAdd(entity, viewModel, authenticatedUser);
      entity.CreatorUserId = authenticatedUser.Id;
    }
  }
}
