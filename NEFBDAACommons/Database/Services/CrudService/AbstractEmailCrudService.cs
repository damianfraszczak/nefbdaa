using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public interface IEmailService : ICrudService<EmailViewModel>
    {
        IEnumerable<EmailViewModel> GetNotSendEmails(AuthenticatedUserViewModel authenticatedUser);
    }
    public class AbstractEmailCrudService : AbstractCrudService<EmailEntity, EmailViewModel, IGenericRepository<EmailEntity>>, IEmailService
    {
        public AbstractEmailCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
        {
        }
        public IEnumerable<EmailViewModel> GetNotSendEmails(AuthenticatedUserViewModel authenticatedUser)
        {
            return _repository
                .GetAll(x => x.DateSend == null)
                .Select(x => MapToViewModel(x))
                .ToList();
        }
    }
}
