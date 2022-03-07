
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;

namespace NEFBDAAStarter.Services.Database
{
  public interface ILanugageCrudService : ICrudService<LanguageViewModel>
  {

  }
 
  [ScopeService]
  public class LanguageCrudService : AbstractCrudService<Language, LanguageViewModel, IGenericRepository<Language>>, ILanugageCrudService
  {
    public LanguageCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
    {
    }
  }
}
