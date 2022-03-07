using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NEFBDAACommons.Configuration;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Security.Models.JwtToken;
using NEFBDAACommons.Security.Services;
using NEFBDAACommons.Web.Extensions;
using NEFBDAANebularStarter.Controlers;
using NEFBDAANebularStarter.Database;
using System.Security.Claims;
namespace NEFBDAACommonsTests.DI
{
  [TestClass]
  public class GenericRepositoryTest
  {
    private static HttpContext httpContext;
    public GenericRepositoryTest()
    {
      DependencyInjectorConfigurator.RegisterServices();
      DependencyInjector.AddDbContextInMemoryDatabase<DatabaseContext>();
      DependencyInjector.GetService<DatabaseContext>().Seed();

      var services = DependencyInjector.Services;
      services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      var loggedUser = DependencyInjector.GetService<IAuthenticationService>().AuthenticateAndGetLoggedUser(new NEFBDAACommons.Security.Models.Auth.AuthenticateUserViewModel()
      {
        Email = "admin@dt.co.uk",
        Password = "admin"
      });
      httpContext = new DefaultHttpContext
      {
        User = new ClaimsPrincipal(new ClaimsIdentity(JwtToken.GetClaims(loggedUser, new System.Collections.Generic.Dictionary<string, string>())))
      };


    }

    [TestMethod]
    public void TestGetGenericRepository()
    {
      
      var controller = new ApiUsersController(
        DependencyInjector.GetService<IMemoryCache>(),
        DependencyInjector.GetService<ICrudServiceUnitOfWork>()
        );
      controller.ControllerContext.HttpContext = httpContext;
      var result = controller.GetMyAccount();
      Assert.AreEqual(result.Content.Id, httpContext.User.GetAuthenticatedUserId());
    }
  }
}
