using NEFBDAACommons.Shared.Helpers;
using System.Collections.Generic;

namespace NEFBDAACommons.DynamicForms.Models
{
    public class FieldDefinitionViewModel
    {
        public string Placeholder { get; set; }
        public string TableTitle { get; set; }
        public string HelpText { get; set; }
        public string GroupName { get; set; }
        public string ParameterName { get; set; }
        public bool Display { get; set; }
        public bool Filterable { get; set; }
        public bool Editable { get; set; }
        public bool EditableOnCreateForm { get; set; } = true;
        public bool EditableOnEditForm { get; set; } = true;
        public bool Exportable { get; set; }
        public int Order { get; set; }
        public int FilterOrder { get; set; }
        public int ExportOrder { get; set; }
        public InputType InputType { get; set; }
        public string QueryType { get; set; }
        public string ModelType { get; set; }
        public string FilterPropertyName { get; set; }
        public bool FilterSelectMultiple { get; set; }
        public string TableWidth { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }

        public override string ToString()
        {
            return $"Field def. {Placeholder}";
        }
    }

    public class OptionsFieldDefinitionViewModel
    {
        public OptionsFieldDefinitionViewModel()
        {
        }

        public OptionsFieldDefinitionViewModel(DisplayFieldDefinitionViewModel x)
        {
            this.ParameterName = x.ParameterName;
            this.Options = x.Options;
        }

        public override string ToString()
        {
            return $"Option {ParameterName}";
        }

        public string ParameterName { get; set; }
        public IEnumerable<ISelectFieldOptionViewModel> Options { get; set; }
    }

    public class DisplayFieldDefinitionViewModel : FieldDefinitionViewModel
    {
        public IEnumerable<ISelectFieldOptionViewModel> Options { get; set; }

        public bool DisplayOnCreationForm { get; set; }
        public bool DisplayOnUpdateForm { get; set; }

        public IEnumerable<FormValidator> Validators { get; set; }

        public DisplayFieldDefinitionViewModel()
        {
        }

        public DisplayFieldDefinitionViewModel(FieldDefinitionViewModel fieldDefinitionViewModel)
        {
            ReflectionHelper.CopyProperties(fieldDefinitionViewModel, this);
        }
    }

    public class EditFieldDefinitionViewModel : DisplayFieldDefinitionViewModel
    {
        public EditFieldDefinitionViewModel()
        {
        }

        public EditFieldDefinitionViewModel(FieldDefinitionViewModel fieldDefinitionViewModel)
        {
            ReflectionHelper.CopyProperties(fieldDefinitionViewModel, this);
        }

        public string VisibilityExpression { get; set; }
        public ComputedConfig ComputedOn { get; set; }
        public FormLayout FormLayout { get; set; }
        public FilteredOptionsConfig OptionsFilteredOn { get; set; }
        public AddInlineConfig AddInlineConfig { get; set; }
        public string[] ExtraClassList { get; set; }
        public string[] VisibilityExpressionParams { get; set; }
    }
}