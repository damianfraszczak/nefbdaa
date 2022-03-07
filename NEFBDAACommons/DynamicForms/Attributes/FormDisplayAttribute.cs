using System;
using System.Collections.Generic;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.DynamicQuery;

namespace NEFBDAACommons.DynamicForms.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormDisplayAttribute : Attribute
    {
        /// <summary>
        /// field display text, if it is set and tabletext not it use this value in both of them
        /// </summary>
        public string FieldText { get; set; }

        /// <summary>
        /// table header display text and FieldText not it use this value in both of them
        /// </summary>
        public string TableText { get; set; }

        public string TableWidth { get; set; }
        public string HelpText { get; set; }

        /// <summary>
        /// css class list applied to input
        /// </summary>
        public string[] ExtraClassList { get; set; }

        /// <summary>
        /// Specify it this propeerty is visible in table view
        /// </summary>
        public bool Display { get; set; } = true;

        public bool Editable { get; set; } = true;
        public bool EditableOnCreateForm { get; set; } = true;
        public bool EditableOnEditForm { get; set; } = true;
        public bool Filterable { get; set; } = true;
        public bool DisplayOnCreationForm { get; set; } = true;
        public bool DisplayOnUpdateForm { get; set; } = true;
        public bool HideOnForms { get; set; } = false;
        public bool Exportable { get; set; } = true;
        public bool Required { get; set; } = false;
        public InputType PredefinedInputType { get; set; } = InputType.Default;

        /// <summary>
        /// It has the highest priority
        /// </summary>
        public bool DisplayNever { get; set; } = false;

        public int Order { get; set; } = 0;
        public string GroupName { get; set; }
        public string[] VisibilityExpressionParams { get; set; }
        public string VisibilityExpression { get; set; }
        public IEnumerable<FormValidator> Validators { get; set; }
        public FormLayout Layout { get; set; } = FormLayout.DEFAULT;
        public string ComputedOnFieldChanged { get; set; }
        public string ComputedOnFromProp { get; set; }
        public string[] VisibleForRoles { get; set; }
        public string FilterPropertyName { get; set; }
        public bool FilterSelectMultiple { get; set; }
        public string FilterQueryType { get; set; } = null;
        public QueryOperatorEnum? FilterQueryOperator { get; set; } = null;
        /// <summary>
        /// Value or referenced property
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        /// Value or referenced property
        /// </summary>
        public string Max { get; set; }
    }
}