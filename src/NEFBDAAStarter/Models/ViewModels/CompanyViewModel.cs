using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.Shared.Models;

namespace NEFBDAAStarter.Models.ViewModels
{
  public class CompanyViewModel : BaseViewModel<long>
  {

    public string Name { get; set; }
    [FormOneToManyAssociationAttribute(AssociationType = typeof(LanguageViewModel), CanBeAddedInline = true)]
    public int LanguageId { get; set; }

    public override string OptionText => Name;
  }
}
