using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEFBDAACommons.Configuration;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAANebularStarter.Database;
using NEFBDAANebularStarter.Models.Entities;
using System;
using System.Collections.Generic;

namespace NEFBDAACommonsTests.Database
{
 // [TestClass]
  public class AppDatabaseUnitOfWorkTest
  {
    public AppDatabaseUnitOfWorkTest()
    {
      DependencyInjectorConfigurator.RegisterServices();
      DependencyInjector.AddDbContextInMemoryDatabase<DatabaseContext>();
      var services = DependencyInjector.Services;
      services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      DatabaseUnitOfWork = DependencyInjector.GetService<IDatabaseUnitOfWork>();
  
    }

    private IDatabaseUnitOfWork DatabaseUnitOfWork { get; }
    [TestMethod]
    public void DatabaseUnitOfWorkRepositoriesSetupTestWithRepository()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.Repository<GenericRepository<AppUser>>());
    }
    [TestMethod]
    public void DatabaseUnitOfWorkRepositoriesSetupTestWithModel()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>());
    }
   
   

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryAny()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().Any());
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryAnyAsynchronous()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().AnyAsync().Result);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryAnyWhere()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().Any(w => w.Id == 1L));
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryAnyWhereAsynchronous()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().AnyAsync(w => w.Id == 1L).Result);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryCount()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().Count() > 0);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryCountAsynchronous()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().CountAsync().Result > 0);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryCountWhere()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().Count(w => w.Id == 1) == 1L);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryCountWhereAsynchronous()
    {
      Assert.IsTrue(DatabaseUnitOfWork.RepositoryByModel<AppUser>().CountAsync(w => w.Id == 1L).Result == 1L);
    }

    
    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryFirstOrDefault()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().FirstOrDefault());
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryFirstOrDefaultAsynchronous()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().FirstOrDefaultAsync().Result);
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryFirstOrDefaultWhere()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().FirstOrDefault(w => w.Id == 1L));
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryFirstOrDefaultWhereAsynchronous()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().FirstOrDefaultAsync(w => w.Id == 1L));
    }



    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryList()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().GetAll());
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryListAsynchronous()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().GetAllAsync());
    }



    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryListPaged()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().GetAll(new PagedListParameters()));
    }

    [TestMethod]
    public void DatabaseUnitOfWorkUserRepositoryListPagedWhere()
    {
      Assert.IsNotNull(DatabaseUnitOfWork.RepositoryByModel<AppUser>().GetAll(new PagedListParameters(), w => w.Id == 1L));
    }



    

    private static AppUser CreateUser()
    {
      var guid = Guid.NewGuid().ToString();

      return new AppUser
      {
        FirstName = $"Name {guid}",
        LastName = $"Surname {guid}",
        Email = $"email{guid}@email.com",
        Password = $"password{guid}",
        Roles = new string[] { AppUserRoleEnum.Admin.ToString() },
        Status = UserStatusEnum.Active
      };
    }

   
  }
}
