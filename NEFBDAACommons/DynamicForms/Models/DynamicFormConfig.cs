using System.Collections.Generic;

namespace NEFBDAACommons.DynamicForms.Models
{
    public enum FormLayout
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        SIX,
        DEFAULT
    }
    public enum FormGroupLayoutComponent
    {
        DEFAULT,
        CARD,
        SIMPLE,
        ACCORDION,
        FIELDSET,
        NONE,
        TABS
    }
    public enum FormMode
    {
        ADD,
        EDIT,
        VIEW
    }

   


    public class FormValidator
    {
        public ValidatorType ValidatorType { get; set; }
        public int Min { get; set; } = int.MinValue;
        public int Max { get; set; } = int.MaxValue;
        public string Regex { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"Validator {ValidatorType}";
        }
    }

    public class FormConfig
    {
        public string ModelType { get; set; }
        public string Title { get; set; }
        public bool PrintLabels { get; set; } = true;
        public FormMode FormMode { get; set; } = FormMode.ADD;
        public FormGroupLayoutComponent FormGroupLayoutComponent { get; set; } = FormGroupLayoutComponent.NONE;
        public FormLayout FormGroupLayout { get; set; } = FormLayout.DEFAULT;
        public IDictionary<string, IEnumerable<FormValidator>> Validators { get; set; }
        public IEnumerable<FormGroupViewModel> FormGroups { get; set; }
    }

    public class FormModel<T>
    {
        public FormConfig FormConfig { get; set; }
        public T Object { get; set; }
    }

    public class BaseFormGroupViewModel
    {
        public string Name { get; set; }
        public bool DisplayName { get; set; } = true;
        public FormLayout FormLayout { get; set; } = FormLayout.DEFAULT;
        public FormLayout DefaultFieldLayout { get; set; } = FormLayout.DEFAULT;
        public string VisibilityExpression { get; set; }
        public string[] VisibilityExpressionParams { get; set; }
        public int Order { get; set; }
        public bool VisibleOnCreateForm { get; set; } = true;
        public bool VisibleOnUpdateForm { get; set; } = true;

        public override string ToString()
        {
            return $"Form Group {Name}";
        }
    }

    public class FormGroupViewModel : BaseFormGroupViewModel
    {
        public EditFieldDefinitionViewModel FormGroupField { get; set; }
        public IEnumerable<EditFieldDefinitionViewModel> EditFields { get; set; }
        public IEnumerable<FormGroupViewModel> FormGroups { get; set; }
    }

    public class UserDefinedFormGroupViewModel : BaseFormGroupViewModel
    {
        public string FormGroupField { get; set; }
        public IEnumerable<string> EditFields { get; set; }
        public IEnumerable<string> FormGroups { get; set; }
    }

    public class FieldOrderViewModel
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public override string ToString()
        {
            return $"Field order {Name}";
        }
    }
}