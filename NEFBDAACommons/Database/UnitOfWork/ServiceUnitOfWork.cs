using System;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Shared.Services;

namespace NEFBDAACommons.Database.UnitOfWork
{

    public interface ICrudServiceUnitOfWork
    {
        T Service<T>();
        ICrudService<TViewModel> ServiceByModel<TModel, TViewModel>() where TModel : class, IEntity, new() where TViewModel : class, new();
    }
    [ScopeService]
    public class CrudServiceUnitOfWork : AbstractService, ICrudServiceUnitOfWork
    {
        public T Service<T>()
        {
            return DependencyInjector.GetService<T>();
        }
       
        public ICrudService<TViewModel> ServiceByModel<TModel, TViewModel>() where TModel : class, IEntity, new() where TViewModel : class, new()
        {
            var globalService = Service<ICrudService<TViewModel>>();
            if (globalService == null)
            {
                DependencyInjector.RegisterService(typeof(ICrudService<TViewModel>), typeof(GenericCrudService<TModel, TViewModel>), DIScope.Scope);
            }
            return Service<ICrudService<TViewModel>>();

        }



    }
}
