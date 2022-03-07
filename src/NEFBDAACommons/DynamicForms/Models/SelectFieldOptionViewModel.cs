using System.Collections.Generic;
using NEFBDAACommons.Shared.Extensions;

namespace NEFBDAACommons.DynamicForms.Models
{
    public interface ISelectFieldOptionViewModel
    {
        string OptionId { get; }
        string OptionText { get; }
        bool HideFromFilter { get; }
        bool HideFromEdit { get; }
        Dictionary<string, string> OptionAdditionalInfo { get; }
    }

    public class SelectFieldOptionViewModel : ISelectFieldOptionViewModel
    {
        public SelectFieldOptionViewModel()
        {
        }

        public SelectFieldOptionViewModel(ISelectFieldOptionViewModel model)
        {
            this.OptionId = model.OptionId;
            this.OptionText = model.OptionText;
            this.OptionAdditionalInfo = model.OptionAdditionalInfo;
            this.HideFromEdit = model.HideFromEdit;
            this.HideFromFilter = model.HideFromFilter;
        }

        public string OptionId { get; set; }
        public string OptionText { get; set; }
        public bool HideFromFilter { get; set; } = false;
        public bool HideFromEdit { get; set; }= false;

        public virtual Dictionary<string, string> OptionAdditionalInfo { get; private set; } =
            new Dictionary<string, string>();

        public virtual void SetAdditionalOptions(object owner)
        {
            var objDict = owner?.ToDictionary();
            if (objDict != null)
            {
                objDict.ForEach(x => OptionAdditionalInfo.TryAdd(x.Key.ToCamelCase(), x.Value.ToString()));
            }
        }
    }
}