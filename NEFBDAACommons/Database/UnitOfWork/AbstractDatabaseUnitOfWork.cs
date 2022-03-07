using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Database.UnitOfWork
{
  public interface IDatabaseUnitOfWork
  {
    T Repository<T>() where T : class;
    IGenericRepository<TModel> RepositoryByModel<TModel>() where TModel : class, IEntity;
    IRepositoryService Repository(Type type);
 

  }

  public class AbstractDatabaseUnitOfWork : IDatabaseUnitOfWork
  {
    #region repositories mapping initilization

    private static Dictionary<Type, Type> _repositoriesMapping = new Dictionary<Type, Type>();

    static AbstractDatabaseUnitOfWork()
    {
      // perform initialization here
      //get all implemented interfaces of class
      //always last is targer interface
      _repositoriesMapping = ReflectionHelper.GetSubclassesOf(typeof(IRepositoryService), true)
          .Where(t => !t.IsAbstract && !t.IsInterface)
          .ToDictionary(t => t.GetInterfaces().Last(), t => t);
    }

    #endregion

    protected Dictionary<string, object> _repositories = new Dictionary<string, object>();


  


    public AbstractDatabaseUnitOfWork()
    {
     
    }

    public IRepositoryService Repository(Type repositoryType)
    {
      var repositoryKey = GetRepositoryKey(repositoryType);
      if (_repositories.Keys.Contains(repositoryKey))
      {
        return _repositories[repositoryKey] as IRepositoryService;
      }
      Type repositoryImplementation = null;
      _repositoriesMapping.TryGetValue(repositoryType, out repositoryImplementation);
      if (repositoryImplementation == null) return null;
      return CreateAndAddRepository(repositoryType, repositoryImplementation) as IRepositoryService;

    }

    public T Repository<T>() where T : class
    {
      return Repository(typeof(T)) as T;
    }

    public IGenericRepository<TModel> RepositoryByModel<TModel>() where TModel : class, IEntity
    {
      var modelType = typeof(TModel);
      var repositoryInterfaceFormModel = _repositoriesMapping.Keys.ToList().FirstOrDefault(repoInterface =>
          repoInterface.GetInterfaces().FirstOrDefault()?.GenericTypeArguments
              .FirstOrDefault(generic_entity_type => generic_entity_type == modelType) != null);

      if (repositoryInterfaceFormModel == null)
      {
        repositoryInterfaceFormModel = typeof(IGenericRepository<>).MakeGenericType(typeof(TModel));
      }


      if (repositoryInterfaceFormModel != null)
      {
        var repositoryKey = GetRepositoryKey(repositoryInterfaceFormModel);
        if (_repositories.Keys.Contains(repositoryKey))
        {
          return _repositories[repositoryKey] as IGenericRepository<TModel>;
        }

        Type repoImplementation = null;
        _repositoriesMapping.TryGetValue(repositoryInterfaceFormModel, out repoImplementation);
        if (repoImplementation == null)
        {
          repoImplementation = typeof(GenericRepository<TModel>);
        }
        var createdrepository = CreateAndAddRepository(repositoryInterfaceFormModel,
            repoImplementation);
        return createdrepository as IGenericRepository<TModel>;
      }
      else
      {
        ApplicationLogger.CreateLogger<AbstractHttpDatabaseUnitOfWork>()
            .LogError($"Can't find repository implementation for model {modelType}, returns null");
        return null;
      }
    }

   



    #region utilities

    private object CreateAndAddRepository(Type repositoryInterface, Type repositoryImplementation)
    {
      var repo = Activator.CreateInstance(repositoryImplementation, GetContext(repositoryImplementation));
      _repositories.TryAdd(GetRepositoryKey(repositoryInterface), repo);
      return repo;
    }

    protected virtual AbstractDbContext GetContext(Type repositoryImplementation)
    {
      return DependencyInjector.GetDbContext();
    }

    private string GetRepositoryKey(Type type)
    {
      return type.FullName;
    }





    #endregion
  }
}
