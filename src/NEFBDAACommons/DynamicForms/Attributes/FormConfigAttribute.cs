using NEFBDAACommons.DynamicForms.Models;

namespace NEFBDAACommons.DynamicForms.Attributes
{
  [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
  public class FormConfigAttribute : System.Attribute
  {
    public string Title { get; set; }
    /// <summary>
    /// Should print labels of fields on form 
    /// </summary>
    public bool PrintLabels { get; set; } = true;
    /// <summary>
    /// Hides default formgroup (withot name)
    /// </summary>
    public bool HideDefaultFormGroup { get; set; } = false;
    /// <summary>
    /// Default size for all form groups
    /// </summary>
    public FormLayout FormGroupLayout { get; set; } = FormLayout.DEFAULT;
    /// <summary>
    /// Defines default size of all fields in form
    /// </summary>
    public FormLayout DefaultFieldLayout { get; set; } = FormLayout.DEFAULT;
    /// <summary>
    /// what type of component to render formgroups should be used
    /// </summary>
    public FormGroupLayoutComponent FormGroupLayoutComponent { get; set; } = FormGroupLayoutComponent.NONE;
    /// <summary>
    /// Defined formgroups in form, si they can be easily hidden
    /// By default it displays all defined by attributes
    /// </summary>
    public string[] FormGroups { get; set; }
  }
}
