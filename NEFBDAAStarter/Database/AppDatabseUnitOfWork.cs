using Microsoft.AspNetCore.Http;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;

namespace NEFBDAAStarter.Database
{
  [ScopeService]
  public class AppDatabseUnitOfWork : AbstractHttpDatabaseUnitOfWork
  {
    public AppDatabseUnitOfWork( IHttpContextAccessor httpAccessor) : base(httpAccessor)
    {
    }
  }
}
