using NEFBDAACommons.DynamicForms.Attributes;

namespace NEFBDAACommons.Shared.Models
{
    public class BasicUserViewModel : BaseViewModel<long>
    {
        [FormDisplay(DisplayOnCreationForm = false, Display = false, Exportable = false)]
        public string UID { get; set; }

        [FormDisplay(Required = true, Layout = DynamicForms.Models.FormLayout.TWO)]
        public string FirstName { get; set; }

        [FormDisplay(Required = true, Layout = DynamicForms.Models.FormLayout.TWO)]
        public string LastName { get; set; }

        [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.Email, Required = true,
            Layout = DynamicForms.Models.FormLayout.TWO)]
        public string Email { get; set; }

        [FormDisplay(PredefinedInputType = DynamicForms.Models.InputType.Tel,
            Layout = DynamicForms.Models.FormLayout.TWO)]
        public string Phone { get; set; }

        [FormDisplay(DisplayNever = true, Editable = false)]
        public string FullName { get; set; }

        [FormDisplay(DisplayNever = true)] public override string OptionText => FullName;

        public override string ToString()
        {
            return OptionText;
        }
    }
}