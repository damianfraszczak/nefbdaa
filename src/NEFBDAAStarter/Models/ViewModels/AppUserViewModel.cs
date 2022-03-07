using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Models;
using NEFBDAAStarter.Models.Entities;
using System;
using System.Collections.Generic;

namespace NEFBDAAStarter.Models.ViewModels
{
    [TableConfig(
        DisplayedFields = new string[] {"Email", "FirstName", "Roles"},
        FilterableFields = new string[] {"OwnerCompanyId", "FirstName", "Email", "Roles"},
        ExportableFields = new string[] {"OwnerCompanyId", "FirstName", "Email", "Roles"}
    )]
    [FormConfig(FormGroupLayoutComponent = FormGroupLayoutComponent.CARD,
        Title = "User",
        FormGroupLayout = FormLayout.TWO,
        HideDefaultFormGroup = true,
        DefaultFieldLayout = FormLayout.ONE)]
    [FormGroupConfig(Name = "Personal info",
        EditFields = new string[]
        {
            "Email", "FirstName", "OwnerId", "OwnerCompanyId", "Roles", "LastName", "Password", "UserDocumentId",
            "LanguageId", "DateOfBirth", "Status222",
            "TestBool", "CompaniesCompanyId", "LanguageName",
            "UserStaticoptions"
        }, Order = 1, FormLayout = FormLayout.TWO)]
    [FormGroupConfig(VisibleOnCreateForm = false, Name = "Conencted",
        FormGroups = new string[] {"Address", "UserImage"}, Order = 10)]
    [FormGroupConfig(Name = "Address", FormGroupField = "AddressVisible", Order = 2, FormLayout = FormLayout.TWO,
        DefaultFieldLayout = FormLayout.THREE, VisibleOnCreateForm = false)]
    [FormGroupConfig(Name = "Second referenced", Order = 3, FormLayout = FormLayout.TWO,
        EditFields = new string[] {"UserTypeId"}, VisibleOnCreateForm = false)]
    [FormGroupConfig(Name = "UserImage", Order = 3, FormLayout = FormLayout.TWO, VisibleOnCreateForm = false)]
    public class AppUserViewModel : BaseUserViewModel
    {
        [FormManyToManyAssociationAttribute(AssociationType = typeof(AppUserRoleEnum))]
        [FormDisplay(Order = 1)]
        public List<string> Roles { get; set; }

        [FormDisplay(PredefinedInputType = InputType.Date)]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfBirth2 { get; set; } = DateTime.Now;

        [FormDisplay(FilterPropertyName = "Companies.CompanyId", Editable = false)]
        [FormManyToManyAssociationAttribute(AssociationType = typeof(CompanyViewModel),
            CanBeAddedInline = true

            // ReferencedPropertyNameToAddInline = "languageId",
            //  ReferencedPropertyValueToAddInline = "languageId",
        )]
        public virtual ICollection<long> CompaniesCompanyId { get; set; }


        [FormDisplay(GroupName = "Address")] public bool AddressVisible { get; set; }

        [FormDisplay(GroupName = "Address", PredefinedInputType = InputType.Html, TableWidth = "150px")]
        public string Address { get; set; }

        [FormDisplay(VisibilityExpression = "object.address", GroupName = "Address", TableText = "Street")]
        public string AddressLine1 { get; set; }

        [FormDisplay(PredefinedInputType = NEFBDAACommons.DynamicForms.Models.InputType.File)]
        public string UserDocumentId { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(LanguageViewModel), CanBeAddedInline = true)]
        public int? LanguageId { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(AppUserViewModel),
            DefaultFilterFieldNames = new string[] {"RolesString"},
            DefaultFilterFieldOperators = new QueryOperatorEnum[] {QueryOperatorEnum.In},
            DefaultFilterFieldValues = new string[] {"User,Coordinator"},
            CanBeAddedInline = true,
            ReferencedPropertyNameToAddInline = "RolesString",
            ReferencedPropertyValueToAddInline = "Admin")]
        public int? OwnerId { get; set; }

        [FormDisplay(FilterPropertyName = "Language.Name", Editable = false)]
        public string LanguageName { get; set; }

        [FormDisplay(PredefinedInputType = InputType.BooleanAsSelect, EditableOnEditForm = false)]
        public bool TestBool { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(CompanyViewModel),
            CanBeAddedInline = true,
            OptionsFilteredOnFieldChanged = "languageId",
            OptionsFilteredOnFieldOperator = QueryOperatorEnum.Equal,
            OptionsFilteredFieldToCompare = "languageId"
        )]
      
        public long? OwnerCompanyId { get; set; }

        [FormDisplay(Editable = false, ComputedOnFieldChanged = "languageId", ComputedOnFromProp = "name")]
        public string LanName { get; set; }

        public long MyProperty { get; set; }

        [FormDisplay(PredefinedInputType = InputType.Select)]
        public UserStatusEnum Status222 { get; set; } = UserStatusEnum.Active;

        [FormDisplay(PredefinedInputType = InputType.Image, GroupName = "UserImage", Required = true,
            VisibilityExpression = "addressVisible === true",
            VisibilityExpressionParams = new string[] {"addressVisible"}
        )]
        public string UserImage { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(UserTypeEnumeration))]
        public long? UserTypeId { get; set; }

        [FormOneToManyAssociationAttribute(AssociationType = typeof(string), StaticOptions = new string[] {"1", "2"})]
        public string UserStaticoptions { get; set; }

        public AppUserViewModel()
        {
        }
    }
}