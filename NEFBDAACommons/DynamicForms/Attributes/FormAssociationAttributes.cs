using NEFBDAACommons.Shared.DynamicQuery;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.DynamicForms.Attributes
{
  [System.AttributeUsage(System.AttributeTargets.Property)]
  public abstract class FormAssociationAttributes : System.Attribute
  {
    public Type AssociationType { get; set; }
    public string[] StaticOptions { get; set; }
    /// <summary>
    /// show select with add button on form
    /// </summary>
    public bool CanBeAddedInline { get; set; } = false;
    /// <summary>
    /// name of property which is important to connect this new value with object to which it is going to belong
    /// for example add user to currently edited company
    /// </summary>
    public string ReferencedPropertyNameToAddInline { get; set; }
    /// <summary>
    /// value of refenced entity
    /// </summary>
    public string ReferencedPropertyValueToAddInline { get; set; }

    public string[] CanBeAddedInlineForRoles { get; set; } = null;
    /// <summary>
    /// if options of this field should be filtered after any other field changed
    /// for example company changed so i can use only these company users to assign a ticket
    /// </summary>
    public string OptionsFilteredOnFieldChanged { get; set; }
    /// <summary>
    /// type of query used to filter options
    /// </summary>
    public QueryOperatorEnum OptionsFilteredOnFieldOperator { get; set; } = QueryOperatorEnum.Equal;
    /// <summary>
    /// value to compare in filter
    /// </summary>
    public string OptionsFilteredFieldToCompare { get; set; }

    public string[] DefaultFilterFieldNames = null;
    public QueryOperatorEnum[] DefaultFilterFieldOperators = null;
    public string[] DefaultFilterFieldValues = null;
   
  }

  public class FormOneToManyAssociationAttributeAttribute : FormAssociationAttributes
  {
  }
  public class FormManyToOneAssociationAttributeAttribute : FormAssociationAttributes
  {
  }

  public class FormManyToManyAssociationAttributeAttribute : FormAssociationAttributes
  {
   
  }
}
