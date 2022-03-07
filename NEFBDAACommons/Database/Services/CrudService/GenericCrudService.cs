using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DynamicForms.Services;

namespace NEFBDAACommons.Database.Services.CrudService
{
    public class GenericCrudService<TModel, TViewModel> : AbstractCrudService<TModel, TViewModel, IGenericRepository<TModel>>
        where TModel : class, IEntity, new()
        where TViewModel : class, new()
    {
        public GenericCrudService()
        {
        }

        public GenericCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
        {
        }
    }
}
