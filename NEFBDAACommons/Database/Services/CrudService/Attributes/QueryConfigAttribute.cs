using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Database.Services.CrudService.Attributes
{
  [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
  public class QueryConfigAttribute : System.Attribute
  {
    public string OrderProperty { get; set; }
    public bool? OrderIsAscending { get; set; }


  }
}
