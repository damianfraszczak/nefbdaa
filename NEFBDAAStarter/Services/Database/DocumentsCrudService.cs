using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Services;

namespace NEFBDAAStarter.Services.Database
{
  [ScopeService]
    public class DocumentsCrudService : AbstractDocumentCrudService
    {
        public DocumentsCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
        {
        }

    }
}
