using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.DynamicForms.Attributes
{
  [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
  public class TableConfigAttribute : System.Attribute
  {
    public string[] DisplayedFields { get; set; }
    public string[] FilterableFields { get; set; }
    public string[] ExportableFields { get; set; }
  }
}
