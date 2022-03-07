using NEFBDAACommons.DynamicForms.Models;

namespace NEFBDAACommons.DynamicForms.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class FormGroupConfigAttribute : System.Attribute
    {
        public bool DisplayName { get; set; } = true;
        public bool VisibleOnCreateForm { get; set; } = true;
        public bool VisibleOnUpdateForm { get; set; } = true;
        public string Name { get; set; }

        /// <summary>
        /// Default size of fields
        /// </summary>
        public FormLayout FormLayout { get; set; } = FormLayout.DEFAULT;
        public FormLayout DefaultFieldLayout { get; set; } = FormLayout.DEFAULT;

        /// <summary>
        /// define condition if group should be visible
        /// for example boolean value of other field in this model AddressVisible to show/hide address form group
        /// </summary>
        public string VisibilityExpression { get; set; }

        public string[] VisibilityExpressionParams { get; set; }

        /// <summary>
        /// any field defined to be a form group field for example checkox to show/hide group based on this selection
        /// </summary>
        public string FormGroupField { get; set; }

        /// <summary>
        /// order of displaying this group in form
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// override which fields are editable
        /// </summary>
        public string[] EditFields { get; set; }

        /// <summary>
        /// defines nested formgroups, used only to put them in one row
        /// </summary>
        public string[] FormGroups { get; set; }

        public FormGroupConfigAttribute()
        {
        }
    }
}