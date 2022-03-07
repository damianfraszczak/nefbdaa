using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.Shared.DynamicQuery;

namespace NEFBDAAStarter.Models.ViewModels
{
  public class LanguageViewModel : BaseViewModel<long>
  {
    public string Name { get; set; }
    [FormOneToManyAssociationAttribute(AssociationType = typeof(CompanyViewModel),
      CanBeAddedInline = true,
      OptionsFilteredOnFieldChanged = "languageId",
      OptionsFilteredOnFieldOperator = QueryOperatorEnum.Equal,
      OptionsFilteredFieldToCompare = "languageId"
    )]
    public long? OwnerCompanyId { get; set; }
    public override string OptionText => Name;
  }
}
