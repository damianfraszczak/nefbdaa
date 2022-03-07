using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.DI.Attributes;
using NEFBDAAStarter.Models.Entities;

namespace NEFBDAAStarter.Services.Repositories
{
    [Repository]
    public class AppUserRepository : AbstractUserRepository<AppUser>
    {
        public AppUserRepository(AbstractDbContext context) : base(context)
        {
        }
    }
}
