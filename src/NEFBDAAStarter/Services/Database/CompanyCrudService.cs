using System.Collections.Generic;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAAStarter.Models.Entities;
using NEFBDAAStarter.Models.ViewModels;

namespace NEFBDAAStarter.Services.Database
{

    public interface ICompanyCrudService : ICrudService<CompanyViewModel>
    {

    }

    [ScopeService]
    public class CompanyCrudService : AbstractCrudService<Company, CompanyViewModel, IGenericRepository<Company>>, ICompanyCrudService
    {
        public CompanyCrudService(IDatabaseUnitOfWork uow, IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService) : base(uow, entityFormFieldDefinitionsService)
        {
        }

        protected override FilterRule GetRuleForSearchByModelType(string modelType, string fieldName)
        {
            return new FilterRule()
            {
                Condition = "and",
                Field = "Name",
                Operator = "like",
                Type = "string",
                Value = "a",
                Rules = new List<FilterRule>()
            };
        }
    }
}
