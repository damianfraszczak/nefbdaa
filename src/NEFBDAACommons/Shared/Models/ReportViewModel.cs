using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.Shared.DynamicQuery;
using System;

namespace NEFBDAACommons.Shared.Models
{
    public class ReportViewModel : BaseViewModel<long>
    {
        public string Name { get; set; }
        // JSON STORED STRING
        public FilterRule Query { get; set; }
        // ENTITY NAME TO GET SERVICE
        public string ServiceType { get; set; }
        // it is visible for all users
        public bool Global { get; set; }
        // creator
        [FormDisplay(Display = false)]
        public long? OwnerId { get; set; }

        public override string OptionText => Name;
    }
}
