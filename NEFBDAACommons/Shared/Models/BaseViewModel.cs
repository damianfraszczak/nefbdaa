using NEFBDAACommons.Database.Models;
using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Shared.Models
{
  public abstract class BaseViewModel<TID> : IEntityWithIdAsStr, IEntityWithId<TID>, ISelectFieldOptionViewModel
  {
    [FormDisplay(DisplayNever = true, Filterable = false)]
    public TID Id { get; set; }
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public Guid? Guid { get; set; }
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public abstract string OptionText { get; }
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public string OptionId => Id + "";
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public bool IsDeleted { get; set; }
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public virtual Dictionary<string, string> OptionAdditionalInfo { get; } = new Dictionary<string, string>();
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public bool HideFromFilter { get; set; } = false;
    [FormDisplay(DisplayNever = true, Filterable = false, Exportable = false)]
    public bool HideFromEdit { get; set; }= false;

    public BaseViewModel()
    {
      Guid = System.Guid.NewGuid();
    }

    public string IdStr()
    {
      return ParsingHelper.GetNotNullString(Id);
    }
    public override string ToString()
    {
      return this.ToInfoString();
    }

  
  }

}
