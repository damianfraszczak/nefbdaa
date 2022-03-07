using NEFBDAACommons.Database.Models;
using NEFBDAACommons.DynamicForms.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NEFBDAACommons.Shared.Models
{
    public class ConfigViewModel : BaseViewModel<long>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AppKey { get; set; }
        [Required]
        public ConfigEntityType Type { get; set; }
        [Required]
        public string Value { get; set; }
        [FormDisplay(Display = false)]
        public string AdditionalInfo { get; set; }
        [FormDisplay(DisplayNever = true)]
        public bool HiddenConfig { get; set; }

        public override string OptionText => Name;
    }
}
