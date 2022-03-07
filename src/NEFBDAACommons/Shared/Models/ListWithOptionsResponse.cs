using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.DynamicForms.Models;
using System.Collections.Generic;

namespace NEFBDAACommons.Shared.Models
{
    public class ListWithOptionsResponse<TViewModel>
        where TViewModel : class
    {
        public IEnumerable<TViewModel> Result { get; set; }
        public IEnumerable<OptionsFieldDefinitionViewModel> Options { get; set; }
    }

    public class PagedListWithOptionsResponse<TViewModel>
        where TViewModel : class
    {
        public PagedList<TViewModel> Result { get; set; }
        public IEnumerable<OptionsFieldDefinitionViewModel> Options { get; set; }
    }
}
