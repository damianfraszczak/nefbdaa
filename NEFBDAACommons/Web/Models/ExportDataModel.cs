using NEFBDAACommons.Shared.DynamicQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Web.Models
{
  public class ExportDataModel
  {
    public string Type { get; set; }
    public string Filename { get; set; }
    public IEnumerable<object> SelectedElements { get; set; }
    public IEnumerable<string> ExportableFields { get; set; }
    public FilterRule Rule { get; set; }
  }
}
