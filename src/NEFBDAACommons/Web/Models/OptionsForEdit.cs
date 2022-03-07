using NEFBDAACommons.Shared.DynamicQuery;

namespace NEFBDAACommons.Web.Models
{
    public class OptionsForEdit
    {
        public object EditedObject { get; set; }
        public FilterRule AdditionalRule { get; set; }
        public string FieldName { get; set; }
        public string ModelType { get; set; }
    }
}