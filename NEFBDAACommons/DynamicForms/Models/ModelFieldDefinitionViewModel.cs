using System.Collections.Generic;

namespace NEFBDAACommons.DynamicForms.Models
{
    public class ModelFieldDefinitionViewModel
    {
        public string ModelType { get; set; }
        public IEnumerable<FieldDefinitionViewModel> FieldDefinitions { get; set; }
    }
}
