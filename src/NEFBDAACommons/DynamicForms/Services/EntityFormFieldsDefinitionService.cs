using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Exceptions;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Internal;

namespace NEFBDAACommons.DynamicForms.Services
{
    #region interfaces and enums

    public interface IEntityFormFieldDefinitionsService
    {
        IEnumerable<FieldDefinitionViewModel> GetModelFieldsDefinitions(string modelType,
            AuthenticatedUserViewModel authenticatedUser, bool forRead = false);

        IEnumerable<FieldDefinitionViewModel> GetModelFieldsDefinitions(Type type,
            AuthenticatedUserViewModel authenticatedUser, bool forRead = false);

        IEnumerable<EditFieldDefinitionViewModel> GetEditFieldsDefinitionViewModel(object editedObject,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<DisplayFieldDefinitionViewModel> GetDisplayFieldsDefinitionViewModel(IEnumerable<object> items,
            Type type,
            AuthenticatedUserViewModel authenticatedUser);

        FormConfig GetEditFormConfig(object editedObject, AuthenticatedUserViewModel authenticatedUser);
        FormModel<object> GetEditFormModel(string modelType, object id, AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<DisplayFieldDefinitionViewModel> GetExportFieldsDefinitionViewModel(Type type,
            IEnumerable<object> objects, AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<DisplayFieldDefinitionViewModel> GetAllFieldsDefinitionViewModel(Type getServiceModelType,
            IEnumerable<object> objects, AuthenticatedUserViewModel authenticatedUser);
    }


    public enum EntityFormFieldDefinitionFilterType
    {
        ADD,
        EDIT,
        VIEW
    }

    public interface IEntityFormFieldDefinitionPreparatorService
    {
        bool CanDisplayField(object editedObject, PropertyInfo propertyInfo, EntityFormFieldDefinitionFilterType type,
            AuthenticatedUserViewModel authenticatedUser);

        bool IsEditable(object editedObject, PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser);

        /// <summary>
        /// return null if all
        /// </summary>
        IEnumerable<string> GetVisibleFields(AuthenticatedUserViewModel authenticatedUser);

        /// <summary>
        /// return null if none
        /// </summary>
        IEnumerable<string> GetExcludeFields(AuthenticatedUserViewModel authenticatedUser);

        /// <summary>
        /// return null if all
        /// </summary>
        IEnumerable<string> GetFilterableFields(AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<string> GetExportableFields(AuthenticatedUserViewModel authenticatedUser);
        IEnumerable<FieldOrderViewModel> GetFieldsOrder(AuthenticatedUserViewModel authenticatedUser);

        /// <summary>
        /// return null if default
        /// </summary>
        IEnumerable<UserDefinedFormGroupViewModel> GetFormGroups(AuthenticatedUserViewModel authenticatedUser);

        List<FormGroupViewModel> ValidateFormGroups(List<FormGroupViewModel> formModel, object editedObject,
            AuthenticatedUserViewModel authenticatedUser);

        FormGroupLayoutComponent? GetFormGroupLayoutType(AuthenticatedUserViewModel authenticatedUser);
        bool CanAddInline(object editedObject, PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser);
    }

    public interface IEntityFormFieldDefinitionModelMapperService
    {
        Type GetModelType(string modelType);
        string GetStringTypeFromType(Type type);
    }

    public interface IEnumValueProvider
    {
        IDictionary<string, string>
            GetValuesForEnum(Type enumerationType, AuthenticatedUserViewModel authenticatedUser);
    }

    class DefaultEnumValueProvider : IEnumValueProvider
    {
        public IDictionary<string, string> GetValuesForEnum(Type enumerationType,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return EnumHelper.GetAll(enumerationType);
        }
    }

    public abstract class
        DefaultEntityFormFieldDefinitionModelMapperService : IEntityFormFieldDefinitionModelMapperService
    {
        private Dictionary<Type, string> mappings;

        protected abstract Dictionary<Type, string> GetModelMappings();

        public Type GetModelType(string modelType)
        {
            var value = GetMappingsInternal().Where(x => x.Value.EqualsIgnoreCase(modelType))
                .Select(x => x.Key).FirstOrDefault();
            ;
            if (value == null)
            {
                throw new BaseException($"{modelType} is not mapped in mappings service");
            }

            return value;
        }

        private Dictionary<Type, string> GetMappingsInternal()
        {
            if (mappings == null)
            {
                mappings = GetModelMappings();
            }

            return mappings;
        }

        public string GetStringTypeFromType(Type type)
        {
            return GetMappingsInternal().GetValueOrDefault(type, null);
        }
    }

    #endregion

    [ScopeService]
    public class EntityFormFieldsDefinitionService : AbstractService, IEntityFormFieldDefinitionsService
    {
        // TODO cache based user and type
        //  private readonly static Dictionary<Type, IEnumerable<FieldDefinitionViewModel>> _cachedDefinitions = new Dictionary<Type, IEnumerable<FieldDefinitionViewModel>>();

        private readonly static InputType[] MULTIPLE_INPUT_TYPES = new InputType[]
        {
            InputType.DualBox, InputType.Radio, InputType.Select, InputType.SelectMultiple
        };

        private readonly IEntityFormFieldDefinitionModelMapperService modelMapperService;
        private IEnumValueProvider enumValueProvider;

        private IServicesProviderLocator _serviceProviderLocator;

        public EntityFormFieldsDefinitionService(
            IEntityFormFieldDefinitionModelMapperService modelMapperService,
            IServicesProviderLocator listProviderLocator)
        {
            this.modelMapperService = modelMapperService;
            this._serviceProviderLocator = listProviderLocator;
        }

        #region form data providers

        private IEntityFormFieldDefinitionPreparatorService
            GetEntityFormFieldDefinitionPreparatorServiceForType(Type type)
        {
            var service = this._serviceProviderLocator.GetEntityFormFieldDefinitionPreparatorService(type);
            if (service == null)
            {
                service = new DefaultEntityFormFieldDefinitionPreparatorService();
            }

            return service;
        }

        public IEnumValueProvider GetEnumValueProvider()
        {
            if (this.enumValueProvider == null)
            {
                this.enumValueProvider =
                    this._serviceProviderLocator.GetServiceFromProvider<IEnumValueProvider>(typeof(IEnumValueProvider));
            }

            if (this.enumValueProvider == null)
            {
                this.enumValueProvider = new DefaultEnumValueProvider();
            }

            return this.enumValueProvider;
        }

        private IEnumerable<ISelectFieldOptionViewModel> GetOptionsForOne(
            object editedObject,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            IEnumerable<ISelectFieldOptionViewModel> result = null;
            if (ReflectionHelper.IsEnumType(propertyInfo.PropertyType))
            {
                result = GetOptionsForEnum(propertyInfo.PropertyType, authenticatedUser);
            }
            else if (ReflectionHelper.IsEnumerationType(propertyInfo.PropertyType))
            {
                result = GetOptionsForEnumeration(propertyInfo.PropertyType, authenticatedUser);
            }
            else if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is
                FormAssociationAttributes faa)
            {
                result = GetOptionsFromAssociation(editedObject, faa, propertyInfo, authenticatedUser);
            }

            return result ?? new List<ISelectFieldOptionViewModel>();
        }

        private IEnumerable<ISelectFieldOptionViewModel> GetOptionsForList(
            IEnumerable<object> objects,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            IEnumerable<ISelectFieldOptionViewModel> result = null;
            if (ReflectionHelper.IsEnumType(propertyInfo.PropertyType))
            {
                result = GetOptionsForEnum(propertyInfo.PropertyType, authenticatedUser);
            }
            else if (ReflectionHelper.IsEnumerationType(propertyInfo.PropertyType))
            {
                result = GetOptionsForEnumeration(propertyInfo.PropertyType, authenticatedUser);
            }
            else if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is
                FormAssociationAttributes faa)
            {
                result = GetOptionsFromAssociation(objects, faa, propertyInfo, authenticatedUser);
            }

            return result ?? new List<ISelectFieldOptionViewModel>();
        }

        private IEnumerable<ISelectFieldOptionViewModel> GetOptionsFromAssociation(
            object objectOrList,
            FormAssociationAttributes faa,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (ReflectionHelper.IsEnumType(faa.AssociationType))
            {
                return GetOptionsForEnum(faa.AssociationType, authenticatedUser);
            }
            else if (ReflectionHelper.IsEnumerationType(faa.AssociationType))
            {
                return GetOptionsForEnumeration(faa.AssociationType, authenticatedUser);
            }
            else if (true == faa.StaticOptions?.Any())
            {
                return faa.StaticOptions.Select(x => new SelectFieldOptionViewModel()
                {
                    OptionId = x,
                    OptionText = x
                });
            }
            else
            {
                var service = this._serviceProviderLocator.GetListProviderService(faa.AssociationType);
                if (service != null)
                {
                    if (ReflectionHelper.IsEnumerableOrArrayType(objectOrList))
                    {
                        var list = objectOrList as IEnumerable<object>;
                        var ids = new List<object>();
                        list
                            .ForEach(x =>
                            {
                                var result = propertyInfo.GetValue(x);
                                if (ReflectionHelper.IsEnumerableOrArrayType(result))
                                {
                                    ids.AddRange(ReflectionHelper.GetAsEnumerable(result).Where(y => y != null));
                                }
                                else if (result != null)
                                {
                                    ids.Add(result);
                                }
                            });
                        return service.GetOptionsForDisplay(ids.Distinct(), propertyInfo, authenticatedUser);
                    }
                    else
                    {
                        return service.GetOptionsForEdit(objectOrList, propertyInfo,
                            service.GetRuleToFilter(objectOrList, propertyInfo, authenticatedUser), authenticatedUser);
                    }
                }
            }

            return new List<ISelectFieldOptionViewModel>();
        }


        private IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEnumeration(Type propertyType,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return Enumeration
                .GetAll<Enumeration>(propertyType)
                .Select(x => new SelectFieldOptionViewModel()
                    {
                        OptionId = x.Id.ToString(),
                        OptionText = x.Name.SplitCamelCase(),
                        HideFromEdit = false,
                        HideFromFilter = false,
                    }
                );
        }


        private IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEnum(Type type,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return GetEnumValueProvider()
                .GetValuesForEnum(ReflectionHelper.GetNullableTypeIfIs(type), authenticatedUser)
                .Select(x =>
                {
                    var enumMetaAttribute = EnumHelper.GetValueAttribute<EnumExtraAttribute>(type, x.Key);
                    return new SelectFieldOptionViewModel()
                    {
                        OptionId = x.Key.ToString(),
                        OptionText = x.Value.Replace("_", " ").ToFriendlySpacedString(),
                        HideFromEdit = enumMetaAttribute?.HideFromEdit ?? false,
                        HideFromFilter = enumMetaAttribute?.HideFromFilter ?? false,
                    };
                });
        }

        #endregion

        #region form fields helpers

        private void SetOrderOfFields(IEnumerable<FieldDefinitionViewModel> result)
        {
            var index = 1000;
            result.ForEach(x =>
            {
                if (x.Order < 1)
                {
                    x.Order = index++;
                }
            });
        }

        private FieldDefinitionViewModel BuildFieldDefinitionForProperty(
            Type type,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            TableConfigAttribute tableConfigAttribute,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var tableText = GetTableText(propertyInfo, authenticatedUser);
            var placeholder = GetPlaceholder(propertyInfo, authenticatedUser);
            var displayFields = tableConfigAttribute?.DisplayedFields;
            var filterableFields = tableConfigAttribute?.FilterableFields ?? tableConfigAttribute?.DisplayedFields;
            var exportableFields = tableConfigAttribute?.ExportableFields ?? tableConfigAttribute?.DisplayedFields;
            var isEditable = GetEditable(ReflectionHelper.CreateInstance<object>(type), preparatorService, propertyInfo,
                authenticatedUser);
            var result = new FieldDefinitionViewModel()
            {
                Display = displayFields?.Any(y => y.EqualsIgnoreCase(propertyInfo.Name))
                          ?? GetDisplayForProperty(preparatorService, propertyInfo, authenticatedUser),
                InputType = GetInputType(propertyInfo, authenticatedUser),
                QueryType = GetQueryType(propertyInfo, authenticatedUser),
                GroupName = GetGroupName(propertyInfo, authenticatedUser),
                ModelType = GetModelType(propertyInfo, authenticatedUser),
                ParameterName = GetParameterName(propertyInfo, authenticatedUser),
                FilterPropertyName = GetFilterPropertyType(propertyInfo, authenticatedUser) ??
                                     GetParameterName(propertyInfo, authenticatedUser),
                Placeholder =
                    ParsingHelper.GetFirstNotEmptyString(placeholder, tableText, GetDefaultFieldText(propertyInfo)),
                TableTitle =
                    ParsingHelper.GetFirstNotEmptyString(tableText, placeholder, GetDefaultFieldText(propertyInfo)),
                TableWidth = GetTableWidth(propertyInfo, authenticatedUser),
                Min = GetMin(propertyInfo, authenticatedUser),
                Max = GetMax(propertyInfo, authenticatedUser),
                HelpText = GetHelpText(propertyInfo, authenticatedUser),
                Editable = isEditable,
                EditableOnCreateForm = GetEditableOnCreateForm(propertyInfo, authenticatedUser, isEditable),
                EditableOnEditForm = GetEditableOnEditForm(propertyInfo, authenticatedUser, isEditable),
                Order = NormalizeFieldOrder(displayFields?.Select(x => x.ToLower())
                            ?.IndexOf(propertyInfo.Name.ToLower()))
                        ?? GetOrderForProperty(type, propertyInfo, authenticatedUser),
                Filterable = filterableFields?.Any(y => y.EqualsIgnoreCase(propertyInfo.Name))
                             ?? GetFilterable(preparatorService, propertyInfo, authenticatedUser),

                Exportable = exportableFields?.Any(y => y.EqualsIgnoreCase(propertyInfo.Name))
                             ?? GetExportable(preparatorService, propertyInfo, authenticatedUser),
                FilterSelectMultiple = GetFilterSelectMultiple(propertyInfo, authenticatedUser)
            };
            result.FilterOrder = NormalizeFieldOrder(filterableFields?.Select(x => x.ToLower())
                ?.IndexOf(propertyInfo.Name.ToLower())) ?? result.Order;
            result.ExportOrder = NormalizeFieldOrder(exportableFields?.Select(x => x.ToLower())
                ?.IndexOf(propertyInfo.Name.ToLower())) ?? result.Order;
            ValidateFieldDefinitionBasedOnUserRole(result, propertyInfo, authenticatedUser);
            return result;
        }


        private bool GetExportable(IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormManyToManyAssociationAttributeAttribute)) is
                FormManyToManyAssociationAttributeAttribute
                mTMAA)
            {
                return false;
            }

            var exportableFields = preparatorService.GetExportableFields(authenticatedUser);
            if (exportableFields != null)
            {
                return exportableFields.Contains(propertyInfo.Name);
            }
            else
            {
                if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fd)
                {
                    return fd.Exportable;
                }
                else
                {
                    return true;
                }
            }
        }

        private bool GetFilterable(IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormManyToManyAssociationAttributeAttribute)) is
                FormManyToManyAssociationAttributeAttribute
                mTMAA)
            {
                return false;
            }

            var filterableFields = preparatorService.GetFilterableFields(authenticatedUser);
            if (filterableFields != null)
            {
                return filterableFields.Contains(propertyInfo.Name);
            }
            else
            {
                if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fd)
                {
                    return fd.Filterable;
                }
                else
                {
                    return true;
                }
            }
        }

        private bool GetEditableOnEditForm(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser,
            bool isEditable)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA)
            {
                return isEditable && dA.EditableOnEditForm;
            }

            return isEditable;
        }

        private bool GetEditableOnCreateForm(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser,
            bool isEditable)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA)
            {
                return isEditable && dA.EditableOnCreateForm;
            }

            return isEditable;
        }

        private string GetTableWidth(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA)
            {
                return dA.TableWidth;
            }

            return null;
        }

        private string GetTableText(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.TableText))
            {
                return dA.TableText;
            }

            return null;
        }

        private string GetPlaceholder(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.FieldText))
            {
                return dA.FieldText;
            }

            return null;
        }

        private string GetFilterPropertyType(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.FilterPropertyName))
            {
                return dA.FilterPropertyName;
            }

            return null;
        }

        private string GetDefaultFieldText(PropertyInfo propertyInfo)
        {
            return GetDefaultFieldText(propertyInfo.Name);
        }

        private string GetDefaultFieldText(string name)
        {
            return name.Replace("ViewModel", "")
                .Replace("Entity", "")
                .SplitCamelCase().Replace(" Id", "");
        }

        private string GetHelpText(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.HelpText))
            {
                return dA.HelpText;
            }

            return null;
        }

        private string GetMin(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.Min))
            {
                return dA.Min;
            }

            return null;
        }

        private string GetMax(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.Max))
            {
                return dA.Max;
            }

            return null;
        }

        private string GetParameterName(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            return propertyInfo.Name.ToCamelCase();
        }

        private string GetQueryType(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.FilterQueryType))
            {
                return dA.FilterQueryType;
            }

            return QueryBuilderHelper.GetQueryTypeString(GetInputType(propertyInfo, authenticatedUser), propertyInfo);
        }

        private string GetGroupName(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA &&
                !string.IsNullOrEmpty(dA.GroupName))
            {
                return dA.GroupName;
            }

            return null;
        }

        private InputType GetInputType(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                if (!InputType.Default.Equals(fDA.PredefinedInputType))
                {
                    return fDA.PredefinedInputType;
                }
            }

            if (propertyInfo.GetCustomAttribute(typeof(FormManyToManyAssociationAttributeAttribute)) is
                FormManyToManyAssociationAttributeAttribute
                mTMAA)
            {
                return InputType.SelectMultiple;
            }

            if (propertyInfo.GetCustomAttribute(typeof(FormManyToOneAssociationAttributeAttribute)) is
                FormManyToOneAssociationAttributeAttribute
                mTOAA)
            {
                return InputType.SelectMultiple;
            }

            if (propertyInfo.GetCustomAttribute(typeof(FormOneToManyAssociationAttributeAttribute)) is
                FormOneToManyAssociationAttributeAttribute
                oTMAA)
            {
                return InputType.Select;
            }

            if (ReflectionHelper.IsEnumType(propertyInfo.PropertyType))
            {
                return InputType.Select;
            }

            if (ReflectionHelper.IsLikeIntegerType(propertyInfo.PropertyType))
            {
                return InputType.Integer;
            }

            if (ReflectionHelper.IsNumericType(propertyInfo.PropertyType))
            {
                return InputType.Number;
            }

            if (ReflectionHelper.IsBooleanType(propertyInfo.PropertyType))
            {
                return InputType.Boolean;
            }

            if (ReflectionHelper.IsDateTimeType(propertyInfo.PropertyType))
            {
                return InputType.Date;
            }


            return InputType.Text;
        }

        private string[] GetExtraClassList(object editedObject, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.ExtraClassList;
            }

            return null;
        }

        private string GetVisibilityExpression(object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.VisibilityExpression;
            }

            return null;
        }

        private string[] GetVisibilityExpressionParams(object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.VisibilityExpressionParams;
            }

            return null;
        }

        private bool GetFilterSelectMultiple(PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.FilterSelectMultiple;
            }

            return false;
        }

        private ComputedConfig GetComputedValueOnExpression(object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                var fieldChanged = fDA.ComputedOnFieldChanged;
                var fromProp = fDA.ComputedOnFromProp;
                if (string.IsNullOrEmpty(fieldChanged) || string.IsNullOrEmpty(fromProp))
                {
                    return null;
                }
                else
                {
                    return new ComputedConfig()
                    {
                        FromProp = fromProp, OnFieldChanged = fieldChanged, SetField = propertyInfo.Name.ToCamelCase()
                    };
                }
            }

            return null;
        }

        private FilteredOptionsConfig GetOptionsFilteredOn(object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is FormAssociationAttributes fDA)
            {
                var fieldChanged = fDA.OptionsFilteredOnFieldChanged;
                var fieldToCompare = fDA.OptionsFilteredFieldToCompare;
                var optionsFilteredOnFieldOperator = fDA.OptionsFilteredOnFieldOperator;

                if (string.IsNullOrEmpty(fieldChanged) || string.IsNullOrEmpty(fieldToCompare))
                {
                    return null;
                }
                else
                {
                    return new FilteredOptionsConfig()
                    {
                        OnFieldChanged = fieldChanged,
                        Operator = optionsFilteredOnFieldOperator,
                        FieldToCompare = fieldToCompare
                    };
                }
            }

            return null;
        }

        private bool GetEditable(
            object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser
        )
        {
            var isEditable = preparatorService.IsEditable(editedObject, propertyInfo, authenticatedUser);
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                var isEditableFromAttribute = fDA.Editable;
                try
                {
                    var isNew = ParsingHelper.GetInt(editedObject.GetEntityIdOrThrowException()) < 1;
                    isEditableFromAttribute &= isNew ? fDA.EditableOnCreateForm : fDA.EditableOnEditForm;
                }
                catch
                {
                }

                return isEditableFromAttribute && isEditable;
            }

            return isEditable;
        }

        private bool GetDisplayOnUpdateForm(
            object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var isViewable = preparatorService.CanDisplayField(editedObject, propertyInfo,
                EntityFormFieldDefinitionFilterType.EDIT, authenticatedUser);
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.DisplayNever ? false : fDA.DisplayOnUpdateForm && isViewable;
            }
            else
            {
                return isViewable;
            }
        }

        private bool GetDisplayOnCreationForm(
            object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser
        )
        {
            var isViewable = preparatorService.CanDisplayField(editedObject, propertyInfo,
                EntityFormFieldDefinitionFilterType.ADD, authenticatedUser);
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.DisplayNever ? false : fDA.DisplayOnCreationForm && isViewable;
            }
            else
            {
                return isViewable;
            }
        }

        private bool GetDisplayForProperty(IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var isViewable = preparatorService.CanDisplayField(null, propertyInfo,
                EntityFormFieldDefinitionFilterType.VIEW, authenticatedUser);
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.DisplayNever ? false : fDA.Display && isViewable;
            }
            else
            {
                return isViewable;
            }
        }

        private int GetOrderForProperty(Type type, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA)
            {
                return fDA.Order;
            }

            return 0;
        }

        private bool FilterWriteProperty(PropertyInfo arg, AuthenticatedUserViewModel authenticatedUser)
        {
            return arg.CanWrite;
        }

        private bool FilterReadProperty(PropertyInfo arg, AuthenticatedUserViewModel authenticatedUser)
        {
            return true;
        }

        #endregion

        #region fields mappings and filters

        public IEnumerable<FieldDefinitionViewModel> GetModelFieldsDefinitions(
            string modelType, AuthenticatedUserViewModel authenticatedUser, bool forRead = false)
            => GetModelFieldsDefinitions(modelMapperService.GetModelType(modelType), authenticatedUser, forRead);

        public IEnumerable<EditFieldDefinitionViewModel> GetEditFieldsDefinitionViewModel(
            object editedObject,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(editedObject?.GetType());
            return editedObject == null
                ? new List<EditFieldDefinitionViewModel>()
                : GetModelFieldsDefinitions(editedObject.GetType(), authenticatedUser, false)
                    .Select(fieldDefinition => MapToEditFieldDefinition(
                        editedObject,
                        filterService,
                        fieldDefinition,
                        authenticatedUser));
        }


        public IEnumerable<DisplayFieldDefinitionViewModel> GetDisplayFieldsDefinitionViewModel(
            IEnumerable<object> items, Type type, AuthenticatedUserViewModel authenticatedUser)
        {
            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(type);
            return GetModelFieldsDefinitions(type, authenticatedUser, false)
                .Select(fieldDefinition => MapToDisplayFieldDefinition(
                    items,
                    type,
                    filterService,
                    fieldDefinition,
                    authenticatedUser));
        }

        private DisplayFieldDefinitionViewModel MapToDisplayFieldDefinition(IEnumerable<object> items, Type type,
            IEntityFormFieldDefinitionPreparatorService filterService, FieldDefinitionViewModel fieldDefinition,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var property = ReflectionHelper.GetPropertyByName(type, fieldDefinition.ParameterName);
            return new DisplayFieldDefinitionViewModel(fieldDefinition)
            {
                Options = GetOptionsForList(items, property, authenticatedUser)
            };
        }

        private EditFieldDefinitionViewModel MapToEditFieldDefinition(
            object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            FieldDefinitionViewModel fieldDefinition,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var property = ReflectionHelper.GetPropertyByName(editedObject, fieldDefinition.ParameterName);
            if (property == null)
            {
                throw new ApiException(
                    $"Property {fieldDefinition.ParameterName} not exists in model {editedObject.GetType().Name}",
                    CommonsConstants.STATUS_CODE_DYNAMIC_FORM_INVALID_CONFIGURATION);
            }

            var result = new EditFieldDefinitionViewModel(fieldDefinition)
            {
                Options = GetOptionsForOne(editedObject, property, authenticatedUser),
                DisplayOnCreationForm =
                    GetDisplayOnCreationForm(editedObject, preparatorService, property, authenticatedUser),
                DisplayOnUpdateForm =
                    GetDisplayOnUpdateForm(editedObject, preparatorService, property, authenticatedUser),
                Editable = GetEditable(editedObject, preparatorService, property, authenticatedUser),
                Validators = GetValidators(editedObject, preparatorService, property, authenticatedUser),
                VisibilityExpression =
                    GetVisibilityExpression(editedObject, preparatorService, property, authenticatedUser),
                VisibilityExpressionParams =
                    GetVisibilityExpressionParams(editedObject, preparatorService, property, authenticatedUser),
                ComputedOn = GetComputedValueOnExpression(editedObject, preparatorService, property, authenticatedUser),
                FormLayout = GetLayout(editedObject, preparatorService, property, authenticatedUser),
                OptionsFilteredOn = GetOptionsFilteredOn(editedObject, preparatorService, property, authenticatedUser),
                AddInlineConfig = GetAddInlineConfig(editedObject, preparatorService, property, authenticatedUser),
                ExtraClassList = GetExtraClassList(editedObject, property, authenticatedUser),
            };
            ValidateFieldDefinitionBasedOnUserRole(result, property, authenticatedUser);
            return result;
        }

        private void ValidateFieldDefinitionBasedOnUserRole(FieldDefinitionViewModel fieldDefinition,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fd)
            {
                if (ParsingHelper.GetBool(fd.VisibleForRoles?.Any()))
                {
                    if (!authenticatedUser.IsInAnyRole(fd.VisibleForRoles))
                    {
                        fieldDefinition.Display = false;
                        fieldDefinition.Filterable = false;
                        fieldDefinition.Editable = false;
                        fieldDefinition.Exportable = false;
                        if (fieldDefinition is EditFieldDefinitionViewModel editFieldDefinition)
                        {
                            editFieldDefinition.DisplayOnCreationForm = false;
                            editFieldDefinition.DisplayOnUpdateForm = false;
                        }
                    }
                }
            }
        }


        private AddInlineConfig GetAddInlineConfig(object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is FormAssociationAttributes fAA &&
                fAA.CanBeAddedInline)
            {
                var isVisibleForRole = fAA.CanBeAddedInlineForRoles == null ||
                                       authenticatedUser.IsInAnyRole(fAA.CanBeAddedInlineForRoles);
                if (isVisibleForRole)
                {
                    return new AddInlineConfig()
                    {
                        ReferencedPropertyName = fAA.ReferencedPropertyNameToAddInline,
                        ReferencedPropertyValue = fAA.ReferencedPropertyValueToAddInline
                    };
                }
            }

            return null;
        }

        private string GetModelType(PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is FormAssociationAttributes fAA)
            {
                if (fAA.StaticOptions != null)
                {
                    return propertyInfo.PropertyType.ToString();
                }

                if (fAA.AssociationType == null)
                {
                    throw new ConfigException(
                        $"{propertyInfo.Name} has not configured AssociationAttribute for {propertyInfo.PropertyType}");
                }

                return this.modelMapperService.GetStringTypeFromType(fAA.AssociationType);
            }

            return null;
        }

        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<FieldDefinitionViewModel> GetModelFieldsDefinitions(Type type,
            AuthenticatedUserViewModel authenticatedUser, bool forRead = false)
        {
            IEnumerable<FieldDefinitionViewModel> result = null;
            //_cachedDefinitions.TryGetValue(type, out result);
            //if (result == null)
            //{
            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(type);
            var excludedFields = filterService.GetExcludeFields(authenticatedUser);
            var visibleFields = filterService.GetVisibleFields(authenticatedUser);
            var tableConfigAttribute = type?.GetCustomAttribute<TableConfigAttribute>();
            result = type?.GetProperties().OrderBy(x => x.MetadataToken)
                .Where(x => forRead
                    ? FilterReadProperty(x, authenticatedUser)
                    : FilterWriteProperty(x, authenticatedUser))
                .Where(x => excludedFields != null ? !excludedFields.Any(y => y.EqualsIgnoreCase(x.Name)) : true)
                .Where(x => visibleFields != null ? visibleFields.Any(y => y.EqualsIgnoreCase(x.Name)) : true)
                .Select(x =>
                    BuildFieldDefinitionForProperty(type, filterService, tableConfigAttribute, x, authenticatedUser))
                .OrderBy(x => x.Order)
                .ToList();
            SetOrderOfFields(result);

            return result;
        }

        public IEnumerable<DisplayFieldDefinitionViewModel> GetAllFieldsDefinitionViewModel(Type type,
            IEnumerable<object> objects, AuthenticatedUserViewModel authenticatedUser)
        {
            IEnumerable<DisplayFieldDefinitionViewModel> result = null;

            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(type);
            var tableConfigAttribute = type?.GetCustomAttribute<TableConfigAttribute>();
            result = type?.GetProperties().OrderBy(x => x.MetadataToken)
                .Select(x =>
                    BuildFieldDefinitionForProperty(type, filterService, tableConfigAttribute, x, authenticatedUser))
                .Select(fieldDefinition => MapToDisplayFieldDefinition(
                    objects,
                    type,
                    filterService,
                    fieldDefinition,
                    authenticatedUser))
                .OrderBy(x => x.Order)
                .ToList();
            SetOrderOfFields(result);
            return result;
        }

        public IEnumerable<DisplayFieldDefinitionViewModel> GetExportFieldsDefinitionViewModel(Type type,
            IEnumerable<object> objects, AuthenticatedUserViewModel authenticatedUser)
        {
            IEnumerable<DisplayFieldDefinitionViewModel> result = null;

            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(type);
            var excludedFields = filterService.GetExcludeFields(authenticatedUser);
            var tableConfigAttribute = type?.GetCustomAttribute<TableConfigAttribute>();
            result = type?.GetProperties().OrderBy(x => x.MetadataToken)
                .Where(x => excludedFields != null ? !excludedFields.Contains(x.Name) : true)
                .Select(x =>
                    BuildFieldDefinitionForProperty(type, filterService, tableConfigAttribute, x, authenticatedUser))
                .Where(x => x.Exportable || true ==
                    tableConfigAttribute?.ExportableFields?.Any(y => y.EqualsIgnoreCase(x.ParameterName)))
                .Select(fieldDefinition => MapToDisplayFieldDefinition(
                    objects,
                    type,
                    filterService,
                    fieldDefinition,
                    authenticatedUser))
                .OrderBy(x => x.ExportOrder)
                .ToList();
            SetOrderOfFields(result);
            return result;
        }

        public FormConfig GetEditFormConfig(object editedObject, AuthenticatedUserViewModel authenticatedUser)
        {
            var editFields = GetEditFieldsDefinitionViewModel(editedObject, authenticatedUser);
            var filterService = GetEntityFormFieldDefinitionPreparatorServiceForType(editedObject?.GetType());
            filterService.GetFieldsOrder(authenticatedUser)?.ForEach(order =>
            {
                var field = editFields.FirstOrDefault(x =>
                    x.ParameterName.Equals(order.Name, StringComparison.InvariantCultureIgnoreCase));
                if (field != null)
                {
                    field.Order = order.Order;
                }
            });

            // the highest priority has service
            List<FormGroupViewModel> formModel = GetUserDefinedFormModel(filterService, editFields, authenticatedUser);
            // if not use attributes to build form
            if (formModel == null)
            {
                formModel = GetFormGroupsBasedOnMetadata(editedObject, authenticatedUser);
            }

            filterService.ValidateFormGroups(formModel, editedObject, authenticatedUser);
            return GetFormConfigBasedOnAttributes(filterService, editedObject, formModel, authenticatedUser);
        }

        private FormConfig GetFormConfigBasedOnAttributes(
            IEntityFormFieldDefinitionPreparatorService filterService,
            object editedObject,
            List<FormGroupViewModel> formModel,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var type = editedObject?.GetType();
            var formConfigAttribute = type?.GetCustomAttribute<FormConfigAttribute>();
            if (formConfigAttribute?.HideDefaultFormGroup == true)
            {
                // hide default form group
                formModel = formModel.Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            }

            var formMode = GetFormMode(editedObject);
            formModel.ForEach(x => { ProcessFormGroup(x, formConfigAttribute, formMode); });
            return new FormConfig()
            {
                ModelType = modelMapperService.GetStringTypeFromType(editedObject.GetType()),
                Title = formConfigAttribute?.Title ?? GetDefaultFieldText(type.Name),
                FormGroups = formModel,
                PrintLabels = formConfigAttribute?.PrintLabels ?? true,
                FormGroupLayout = formConfigAttribute?.FormGroupLayout ?? FormLayout.DEFAULT,
                FormGroupLayoutComponent = filterService.GetFormGroupLayoutType(authenticatedUser) ??
                                           formConfigAttribute?.FormGroupLayoutComponent ??
                                           FormGroupLayoutComponent.DEFAULT,
                FormMode = formMode,
            };
        }

        private void ProcessFormGroup(FormGroupViewModel formGroup, FormConfigAttribute formConfigAttribute,
            FormMode formMode)
        {
            if (formMode == FormMode.ADD && !formGroup.VisibleOnCreateForm)
            {
                return;
            }

            if (formMode == FormMode.EDIT && !formGroup.VisibleOnUpdateForm)
            {
                return;
            }

            // TODO make it better
            formGroup.FormGroups?.ForEach(fg => { ProcessFormGroup(fg, formConfigAttribute, formMode); });

            if (formGroup.FormLayout == FormLayout.DEFAULT)
            {
                formGroup.FormLayout = formConfigAttribute?.FormGroupLayout ?? FormLayout.DEFAULT;
            }


            formGroup.EditFields.ForEach(y =>
            {
                if (y.FormLayout != FormLayout.DEFAULT) return;
                if (formGroup.DefaultFieldLayout == FormLayout.DEFAULT)
                {
                    y.FormLayout = formConfigAttribute?.DefaultFieldLayout ?? y.FormLayout;
                }
                else
                {
                    y.FormLayout = formGroup.DefaultFieldLayout;
                }
            });
        }

        private List<FormGroupViewModel> GetUserDefinedFormModel(
            IEntityFormFieldDefinitionPreparatorService filterService,
            IEnumerable<EditFieldDefinitionViewModel> editFields,
            AuthenticatedUserViewModel authenticatedUser)
        {
            List<FormGroupViewModel> formModel = null;
            var userDefinedFormGroups = filterService.GetFormGroups(authenticatedUser);
            if (userDefinedFormGroups != null)
            {
                formModel = new List<FormGroupViewModel>();
                userDefinedFormGroups.ForEach(userGroup =>
                {
                    var fgField = string.IsNullOrEmpty(userGroup.FormGroupField)
                        ? null
                        : editFields.FirstOrDefault(y =>
                            y.ParameterName.Equals(userGroup.FormGroupField,
                                StringComparison.InvariantCultureIgnoreCase));

                    formModel.Add(new FormGroupViewModel()
                    {
                        Name = userGroup.Name,
                        FormLayout = userGroup.FormLayout,
                        DefaultFieldLayout = userGroup.DefaultFieldLayout,
                        DisplayName = userGroup.DisplayName,
                        EditFields = editFields.Where(x => x != fgField && userGroup.EditFields
                                .FirstOrDefault(y =>
                                    x.ParameterName.Equals(y,
                                        StringComparison.InvariantCultureIgnoreCase)) !=
                            null),
                        FormGroupField = fgField,
                        Order = fgField.Order,
                        VisibilityExpression = fgField.VisibilityExpression,
                    });
                });

                userDefinedFormGroups.Where(x => true == x.FormGroups?.Any()).ForEach(cg =>
                {
                    var composedGroup = formModel.FirstOrDefault(x => x.Name.EqualsAnyIgnoreCase(cg.Name));
                    if (composedGroup != null)
                    {
                        composedGroup.FormGroups = formModel.Where(x => cg.FormGroups.Contains(x.Name));
                        composedGroup.FormGroups.ForEach(x => formModel.Remove(x));
                    }
                });
            }

            return formModel;
        }


        #region build formgroups

        public List<FormGroupViewModel> GetFormGroupsBasedOnMetadata(object editedObject,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var formMode = GetFormMode(editedObject);
            var usedEditFields = new List<EditFieldDefinitionViewModel>();
            var editFields = GetEditFieldsDefinitionViewModel(editedObject, authenticatedUser).ToList();
            var attributeDefinedFormGroups = GetDefinedByAttribteFormGroups(editedObject)?.ToList();
            var usedFormGroups = new List<string>();

            var result = new List<FormGroupViewModel>();
            foreach (var definedAttribute in attributeDefinedFormGroups)
            {
                if (definedAttribute.EditFields?.Count() > 0)
                {
                    var visibleOnCreate = definedAttribute?.VisibleOnCreateForm ?? true;
                    var visibleOnUpdate = definedAttribute?.VisibleOnUpdateForm ?? true;
                    if (!visibleOnCreate && formMode == FormMode.ADD)
                    {
                        continue;
                    }

                    if (!visibleOnUpdate && formMode == FormMode.EDIT)
                    {
                        continue;
                    }

                    var definedFieldLowerCase = definedAttribute.EditFields.Select(x => x.ToLower()).ToList();
                    var fgField = string.IsNullOrEmpty(definedAttribute?.FormGroupField)
                        ? null
                        : editFields.FirstOrDefault(y =>
                            y.ParameterName.EqualsAnyIgnoreCase(definedAttribute?.FormGroupField));
                    // order based on order in this field .OrderBy(y => y.Order)
                    var groupEditFields = editFields.Where(y =>
                            y != fgField &&
                            definedAttribute.EditFields.Any(k => k.EqualsAnyIgnoreCase(y.ParameterName)))
                        .Select(x =>
                        {
                            x.Order = NormalizeFieldOrder(definedFieldLowerCase.IndexOf(x.ParameterName.ToLower())) ??
                                      0;
                            return x;
                        })
                        .OrderBy(x => x.Order)
                        .ToList();
                    result.Add(new FormGroupViewModel()
                    {
                        Name = definedAttribute.Name,
                        EditFields = groupEditFields,
                        Order = definedAttribute?.Order ?? 0,
                        FormGroupField = fgField,
                        VisibilityExpression = definedAttribute?.VisibilityExpression,
                        FormLayout = definedAttribute?.FormLayout ?? FormLayout.DEFAULT,
                        DefaultFieldLayout = definedAttribute?.DefaultFieldLayout ?? FormLayout.DEFAULT,
                        DisplayName = definedAttribute?.DisplayName ?? true,
                        VisibleOnCreateForm = visibleOnCreate,
                        VisibleOnUpdateForm = visibleOnUpdate,
                    });
                    // editFields.RemoveAll(x =>
                    //     groupEditFields.Any(y => y.ParameterName.EqualsAnyIgnoreCase(x.ParameterName)));

                    usedFormGroups.Add(definedAttribute.Name);
                    if (fgField != null)
                    {
                        usedEditFields.Add(fgField);
                    }

                    usedEditFields.AddRange(groupEditFields);
                }
            }

            editFields.RemoveAll(x => usedEditFields.Any(y => y.ParameterName.EqualsAnyIgnoreCase(x.ParameterName)));
            var groupedFieldByGroupName = editFields
                .GroupBy(x => x.GroupName)
                .ToDictionary(x => x.Key ?? "", x => x.ToList());

            groupedFieldByGroupName
                .Where(x => !usedFormGroups.Contains(x.Key))
                .ForEach(x =>
                {
                    var definedAttribute = attributeDefinedFormGroups.FirstOrDefault(at =>
                        at.Name.Equals(x.Key, StringComparison.InvariantCultureIgnoreCase));
                    var fgField = string.IsNullOrEmpty(definedAttribute?.FormGroupField)
                        ? null
                        : x.Value.FirstOrDefault(y => y.ParameterName.Equals(definedAttribute?.FormGroupField,
                            StringComparison.InvariantCultureIgnoreCase));
                    var fg = new FormGroupViewModel()
                    {
                        Name = x.Key,
                        EditFields = x.Value
                            .Where(y => y != fgField &&
                                        !usedEditFields.Any(k => k.ParameterName.EqualsAnyIgnoreCase(y.ParameterName)))
                            .OrderBy(y => y.Order).ToList(),
                        Order = definedAttribute?.Order ?? 0,
                        FormGroupField = fgField,
                        VisibilityExpression = definedAttribute?.VisibilityExpression,
                        DefaultFieldLayout = definedAttribute?.DefaultFieldLayout ?? FormLayout.DEFAULT,
                        FormLayout = definedAttribute?.FormLayout ?? FormLayout.DEFAULT,
                        DisplayName = definedAttribute?.DisplayName ?? true,
                        VisibleOnCreateForm = definedAttribute?.VisibleOnCreateForm ?? true,
                        VisibleOnUpdateForm = definedAttribute?.VisibleOnUpdateForm ?? true,
                    };
                    result.Add(fg);
                    if (fgField != null)
                    {
                        usedEditFields.Add(fgField);
                    }

                    // usedEditFields.AddRange(fg.EditFields);
                });


            // composed form groups
            attributeDefinedFormGroups.Where(x => x.FormGroups != null && x.FormGroups.Any()).ForEach(cg =>
            {
                var composedGroup = new FormGroupViewModel()
                {
                    Name = cg.Name,
                    EditFields = new List<EditFieldDefinitionViewModel>(),
                    Order = cg.Order,
                    VisibleOnCreateForm = cg.VisibleOnCreateForm,
                    VisibleOnUpdateForm = cg.VisibleOnUpdateForm,
                    DefaultFieldLayout = cg?.DefaultFieldLayout ?? FormLayout.DEFAULT,
                };
                composedGroup.FormGroups =
                    result.Where(x => cg.FormGroups.Contains(x.Name)).OrderBy(x => x.Order).ToList();
                composedGroup.FormGroups.ForEach(x => result.Remove(x));
                result.Add(composedGroup);
            });


            return result.OrderBy(x => x.Order).ToList();
        }

        private int? NormalizeFieldOrder(int? indexOf)
        {
            if (indexOf.HasValue && indexOf >= 0)
            {
                return indexOf.Value + 1;
            }
            else
            {
                return indexOf;
            }
        }


        private IEnumerable<UserDefinedFormGroupViewModel> GetDefinedByAttribteFormGroups(object editedObject)
        {
            var type = editedObject?.GetType();
            var result = new List<UserDefinedFormGroupViewModel>();
            var groupAttributes = type?.GetCustomAttributes<FormGroupConfigAttribute>();
            if (groupAttributes != null)
            {
                groupAttributes.ForEach(attr =>
                {
                    result.Add(new UserDefinedFormGroupViewModel()
                    {
                        FormGroupField = attr.FormGroupField,
                        FormLayout = attr.FormLayout,
                        DefaultFieldLayout = attr.DefaultFieldLayout,
                        Name = attr.Name,
                        DisplayName = attr.DisplayName,
                        VisibleOnCreateForm = attr.VisibleOnCreateForm,
                        VisibleOnUpdateForm = attr.VisibleOnUpdateForm,
                        Order = attr.Order,
                        VisibilityExpression = attr.VisibilityExpression,
                        VisibilityExpressionParams = attr.VisibilityExpressionParams,
                        EditFields = attr.EditFields,
                        FormGroups = attr.FormGroups
                    });
                });
            }

            return result;
        }

        #endregion


        #region form config

        private IEnumerable<FormValidator> GetValidators(
            object editedObject,
            IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser
        )
        {
            var result = new LinkedList<FormValidator>();
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute fDA &&
                fDA.Required)
            {
                result.AddLast(new FormValidator() {ValidatorType = ValidatorType.Required});
                if (fDA.Validators != null)
                {
                    fDA.Validators.ForEach(validator => result.AddLast(validator));
                }
            }

            if (GetInputType(propertyInfo, authenticatedUser) == InputType.Email)
            {
                result.AddLast(new FormValidator() {ValidatorType = ValidatorType.Email});
            }

            if (ReflectionHelper.IsLikeIntegerType(propertyInfo.PropertyType))
            {
                result.AddLast(new FormValidator() {ValidatorType = ValidatorType.TotalNumber});
            }


            return result;
        }

        private FormMode GetFormMode(object editedObject)
        {
            var id = (editedObject as IEntityWithIdAsStr)?.IdStr();
            if (ParsingHelper.GetInt(id) > 0)
            {
                return FormMode.EDIT;
            }
            else
            {
                return FormMode.ADD;
            }
        }

        private FormLayout GetLayout(object editedObject, IEntityFormFieldDefinitionPreparatorService preparatorService,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
        {
            if (propertyInfo.GetCustomAttribute(typeof(FormDisplayAttribute)) is FormDisplayAttribute dA)
            {
                return dA.Layout;
            }

            return FormLayout.DEFAULT;
        }

        public FormModel<object> GetEditFormModel(string modelTypeString, object id,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var modelType = modelMapperService.GetModelType(modelTypeString);
            if (modelType == null)
            {
                throw new ApiException($"Model mapping for {modelTypeString} doesn't exists");
            }

            // TODO better for crud service
            var service = _serviceProviderLocator.GetListProviderService(modelType);
            var model = ReflectionHelper.CreateInstance<object>(modelType);

            if (service != null && service is IRawCrudService)
            {
                try
                {
                    // i have service for it so i can find by id
                    model = (service as IRawCrudService).GetById(id, authenticatedUser);
                }
                catch (Exception ex)
                {
                    ApplicationLogger.LogError(this, $"Error while getting form config for object", ex);
                }
            }

            return new FormModel<object>()
            {
                FormConfig = GetEditFormConfig(model, authenticatedUser),
                Object = model
            };
        }

        #endregion
    }
}

class DefaultEntityFormFieldDefinitionPreparatorService : IEntityFormFieldDefinitionPreparatorService
{
    public bool CanDisplayField(object editedObject, PropertyInfo propertyInfo,
        EntityFormFieldDefinitionFilterType type, AuthenticatedUserViewModel authenticatedUser)
    {
        return true;
    }

    public bool IsEditable(object editedObject, PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
    {
        return true;
    }

    public IEnumerable<string> GetExcludeFields(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public IEnumerable<string> GetFilterableFields(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public IEnumerable<string> GetVisibleFields(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public IEnumerable<FieldOrderViewModel> GetFieldsOrder(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public IEnumerable<UserDefinedFormGroupViewModel> GetFormGroups(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public FormLayout? GetFormLayout(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public List<FormGroupViewModel> ValidateFormGroups(List<FormGroupViewModel> formModel, object editedObject,
        AuthenticatedUserViewModel authenticatedUser)
    {
        return formModel;
    }

    public IEnumerable<string> GetExportableFields(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public FormGroupLayoutComponent? GetFormGroupLayoutType(AuthenticatedUserViewModel authenticatedUser)
    {
        return null;
    }

    public bool CanAddInline(object editedObject, PropertyInfo propertyInfo,
        AuthenticatedUserViewModel authenticatedUser)
    {
        return true;
    }
}