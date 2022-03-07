using NEFBDAACommons.Shared.DynamicQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.DynamicForms.Models
{
  public class FilteredOptionsConfig
  {
    public string OnFieldChanged { get; set; }
    public QueryOperatorEnum Operator { get; set; }
    public string FieldToCompare { get; internal set; }
  }
}
