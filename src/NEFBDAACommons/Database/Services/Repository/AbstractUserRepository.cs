using NEFBDAACommons.Security.Models;
using NEFBDAACommons.Security.Models.Auth;

namespace NEFBDAACommons.Database.Services.Repository
{
  public interface IUserRepository<T> : IGenericRepository<T> where T : BaseUser
  {
    T Authenticate(AuthenticateUserViewModel authenticateUserViewModel);
    T GetByEmail(string email);
    T GetByPhone(string phone);
  }
  public class AbstractUserRepository<T> : GenericRepository<T>, IUserRepository<T> where T : BaseUser
  {
    public AbstractUserRepository(AbstractDbContext context) : base(context)
    {
    }

    public T Authenticate(AuthenticateUserViewModel authenticateUserViewModel)
    {
      return FirstOrDefault
           (
               user => (user.Email != null && user.Email.Equals(authenticateUserViewModel.Email) ||
               user.Phone != null && user.Phone.Equals(authenticateUserViewModel.Phone) ||
                user.UID != null && user.UID.Equals(authenticateUserViewModel.UID))
               && user.Password.Equals(authenticateUserViewModel.Password)
               && user.Status == UserStatusEnum.Active
               && !user.IsDeleted
           );
    }

    public T GetByEmail(string email)
    {
      return FirstOrDefault
           (
               user => user.Email.Equals(email)
               && user.Status == UserStatusEnum.Active
               && !user.IsDeleted
           );
    }
    public T GetByPhone(string phone)
    {
      return FirstOrDefault
           (
               user => user.Phone.Equals(phone)
               && user.Status == UserStatusEnum.Active
               && !user.IsDeleted
           );
    }
  }
}

