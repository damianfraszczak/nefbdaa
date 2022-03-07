using System;

namespace NEFBDAACommons.Database.Services.CrudService.Attributes
{
  [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
  public class SetDefaultValueAttribute : Attribute
  {
    public bool Always { get; set; } = false;
  }
}
