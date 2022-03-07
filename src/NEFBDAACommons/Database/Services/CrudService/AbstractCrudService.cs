using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.Database.Services.CrudService.Attributes;
using NEFBDAACommons.Database.Services.Repository;
using NEFBDAACommons.Database.UnitOfWork;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NEFBDAACommons.DynamicForms.Attributes;
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Database.Services
{
    public abstract class AbstractCrudService<TModel, TViewModel, TRepository> : AbstractService,
        ICrudService<TViewModel>, IEntityListProviderService, IEntityFormFieldDefinitionPreparatorService
        where TRepository : class, IGenericRepository<TModel>
        where TModel : class, IEntity, new()
        where TViewModel : class, new()

    {
        protected readonly IDatabaseUnitOfWork _uow;
        protected readonly TRepository _repository;
        protected readonly IEntityFormFieldDefinitionsService _entityFormFieldDefinitionsService;

        public AbstractCrudService() : this(
            DependencyInjector.GetService<IDatabaseUnitOfWork>(),
            DependencyInjector.GetService<IEntityFormFieldDefinitionsService>())
        {
        }

        public AbstractCrudService(IDatabaseUnitOfWork uow,
            IEntityFormFieldDefinitionsService entityFormFieldDefinitionsService)
        {
            this._uow = uow;
            this._repository = this._uow.Repository<TRepository>();
            this._entityFormFieldDefinitionsService = entityFormFieldDefinitionsService;
            if (this._repository == null)
            {
                this._repository = this._uow.RepositoryByModel<TModel>() as TRepository;
            }
        }

        #region crud

        #region get

        public virtual IEnumerable<TViewModel> GetAll(AuthenticatedUserViewModel authenticatedUser)
            => MapToViewModelList(_repository.GetAll(
                FilterForGetRequest(authenticatedUser),
                GetDefaultOrder(),
                IncludesForGetListRequestParams(authenticatedUser)));

        public virtual IEnumerable<TViewModel> GetAll(FilterRule rule, AuthenticatedUserViewModel authenticatedUser)
            => MapToViewModelList(_repository.GetAll(BuildGetAllExpressionForRule(rule, authenticatedUser),
                GetDefaultOrder(),
                IncludesForGetListRequestParams(authenticatedUser)));

        public virtual FilterRule GetRuleToFilter(object objectOrList, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            FilterRule rule = null;
            if (propertyInfo.GetCustomAttribute(typeof(FormAssociationAttributes)) is FormAssociationAttributes fAA)
            {
                var fieldNames = fAA.DefaultFilterFieldNames ?? new string[0];
                var fieldOperators = fAA.DefaultFilterFieldOperators ?? new QueryOperatorEnum[0];
                var values = fAA.DefaultFilterFieldValues ?? new string[0];
                if (fieldNames.Length == fieldOperators.Length && fieldOperators.Length == values.Length)
                {
                    rule = QueryBuilderHelper.GetEmptyRule();
                    for (var i = 0; i < fieldNames.Length; i++)
                    {
                        var fieldName = fieldNames[i];
                        var fieldOperator = fieldOperators[i];
                        object value = values[i];
                        try
                        {
                            var propetyOrValue = value?.ToString();
                            var destinationValue = ReflectionHelper.GetPropValue(objectOrList, propetyOrValue);
                            var propertyType = ReflectionHelper.GetNullableTypeIfIs(propertyInfo.PropertyType);
                            if (destinationValue == null || (
                                destinationValue.GetType() != propertyType
                            ))
                            {
                                destinationValue = value;
                            }

                            if (fieldOperator == QueryOperatorEnum.In || fieldOperator == QueryOperatorEnum.Not_In)
                            {
                                if (!ReflectionHelper.IsEnumerableOrArrayType(value))
                                {
                                    value = value.ToString().Split(",");
                                }
                            }

                            var ruleToAdd = QueryBuilderHelper.BuildFilterRuleByReflection(GetModelType(),
                                new FilterRuleByReflection()
                                {
                                    Operator = fieldOperator,
                                    Value = destinationValue,
                                    FieldName = fieldName
                                });
                            ruleToAdd = ValidateRule(propertyInfo, ruleToAdd, fieldName, fieldOperator, value);
                            if (ruleToAdd != null)
                            {
                                rule.Rules.Add(ruleToAdd);
                            }
                        }
                        catch (Exception ex)
                        {
                            ApplicationLogger.LogError(this,
                                $"Error with generating default rule {fieldName} {fieldOperator} {value}", ex);
                        }
                    }
                }
                else
                {
                    ApplicationLogger.LogDebug(this,
                        $"error with configuring default filters for {objectOrList?.GetType()} with {fAA.AssociationType}");
                }
            }

            return rule;
        }

        protected virtual FilterRule ValidateRule(PropertyInfo propertyInfo, FilterRule ruleToAdd, string fieldName,
            QueryOperatorEnum fieldOperator, object value)
        {
            if (ReflectionHelper.IsEnumerableOrArrayType(value)
                && ruleToAdd.Type.EqualsIgnoreCase("string")
                && (fieldOperator == QueryOperatorEnum.In || fieldOperator == QueryOperatorEnum.Not_In))
            {
                var valAsEnumerable = ReflectionHelper.GetAsEnumerable(value);
                if (valAsEnumerable != null)
                {
                    ruleToAdd = new FilterRule()
                    {
                        Condition = "or",
                        Rules = new List<FilterRule>()
                    };
                    fieldOperator = fieldOperator == QueryOperatorEnum.In
                        ? QueryOperatorEnum.Contains
                        : QueryOperatorEnum.Not_Contains;
                    valAsEnumerable.ForEach(val =>
                    {
                        ruleToAdd.Rules.Add(QueryBuilderHelper.BuildFilterRuleByReflection(GetModelType(),
                            new FilterRuleByReflection()
                            {
                                Operator = fieldOperator,
                                Value = val?.ToString()?.Trim(),
                                FieldName = fieldName,
                            }));
                    });
                }
            }

            return ruleToAdd;
        }

        public virtual IEnumerable<TViewModel> GetAllByIds(IEnumerable<object> ids,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (ids.Any())
            {
                return MapToViewModelList(
                    _repository.GetAll(BuildGetAllExpressionForRule(new FilterRule()
                        {
                            Condition = "and",
                            Rules = new List<FilterRule>
                            {
                                new FilterRule
                                {
                                    Condition = "and",
                                    Field = "Id",
                                    Id = "Id",
                                    Input = "NA",
                                    Operator = "in",
                                    Type = "integer",
                                    Value = ids
                                }
                            }
                        }, authenticatedUser), GetDefaultOrder(),
                        IncludesForGetListRequestParams(authenticatedUser)));
            }
            else
            {
                return new List<TViewModel>();
            }
        }


        public virtual PagedList<TViewModel> GetPaged(PagedListParameters pagedParams,
            AuthenticatedUserViewModel authenticatedUser)
            => MapToPagedList(_repository.GetAll(VerifyPagedParams(pagedParams), FilterForGetRequest(authenticatedUser),
                IncludesForGetListRequestParams(authenticatedUser)));


        public virtual PagedList<TViewModel> GetPaged(FilterRule rule, PagedListParameters pagedParams,
            AuthenticatedUserViewModel authenticatedUser)
            => MapToPagedList(_repository.GetAll(VerifyPagedParams(pagedParams),
                BuildGetAllExpressionForRule(rule, authenticatedUser),
                IncludesForGetListRequestParams(authenticatedUser)));


        public virtual TViewModel GetById(object id, AuthenticatedUserViewModel authenticatedUser)
        {
            var entity = _repository.GetById(VerifyEntityId(id), IncludesForGetOneRequestParams(authenticatedUser));
            CheckIfUserIsAuthorizedIfNotThrowException(OperationTypeEnum.GET, entity, authenticatedUser);
            return MapToViewModel(entity);
        }

        public TViewModel GetByRule(FilterRule rule, AuthenticatedUserViewModel authenticatedUser)
            => MapToViewModel(_repository.FirstOrDefault(BuildGetAllExpressionForRule(rule, authenticatedUser),
                IncludesForGetOneRequestParams(authenticatedUser)));

        public virtual FormModel<TViewModel> GetWithEditDataById<TID>(TID id,
            Dictionary<string, string> additionalParams, AuthenticatedUserViewModel authenticatedUser)
        {
            var result = new FormModel<TViewModel>() { };
            try
            {
                result.Object = GetById(id, authenticatedUser);
            }
            catch (Exception)
            {
            }

            if (result.Object == null)
            {
                result.Object = new TViewModel();
            }

            UpdateObjectByAdditionalParams(result.Object, additionalParams);
            //result.EditFieldDefinitionModels = formModel.EditFieldDefinitions;
            result.FormConfig = _entityFormFieldDefinitionsService.GetEditFormConfig(result.Object, authenticatedUser);
            return result;
        }

        // TODO check caching query
        public virtual IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEdit(OptionsForEdit optionsForEdit,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var mapperService = DependencyInjector.GetService<IEntityFormFieldDefinitionModelMapperService>();
            var modelType = mapperService.GetModelType(optionsForEdit.ModelType);
            // TODO pass model type
            var serialziedDynamicObject = SerializationHelper.JsonSerialize(optionsForEdit.EditedObject);
            var editedObject = SerializationHelper.JsonDeserialize(serialziedDynamicObject, modelType);
            var propertyInfo =
                ReflectionHelper.GetPropertyByName(editedObject, optionsForEdit.FieldName);
            var defaultRule = GetRuleToFilter(editedObject, propertyInfo, authenticatedUser);
            if (optionsForEdit.AdditionalRule != null && !optionsForEdit.AdditionalRule.IsEmpty())
            {
                if (defaultRule == null)
                {
                    defaultRule = optionsForEdit.AdditionalRule;
                }
                else
                {
                    defaultRule.Rules.Add(optionsForEdit.AdditionalRule);
                }
            }

            var currentValue = ReflectionHelper.GetPropValue(editedObject, propertyInfo.Name);
            var currentValueAsLong = ParsingHelper.GetLong(currentValue?.ToString());
            // load by default current assigned value even if it is deleted
            if (currentValue != null && currentValueAsLong > 0)
            {
                defaultRule = new FilterRule()
                {
                    Condition = "or",
                    Rules = new List<FilterRule>()
                    {
                        defaultRule, new FilterRule()
                        {
                            Condition = "and",
                            Field = GetIdPropertyName(),
                            Operator = "=",
                            Type = QueryBuilderHelper.GetQueryTypeString(propertyInfo),
                            Value = currentValue
                        }
                    }
                };
            }

            return GetOptionsForEdit(editedObject, propertyInfo, defaultRule, authenticatedUser);
        }

        protected virtual string GetIdPropertyName()
        {
            return "Id";
        }

        public virtual IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEdit(object editedObject,
            PropertyInfo propertyInfo, FilterRule rule, AuthenticatedUserViewModel authenticatedUser)
            => GetAll(rule, authenticatedUser)
                .Where(x => x is ISelectFieldOptionViewModel)
                .Select(x => new SelectFieldOptionViewModel(x as ISelectFieldOptionViewModel));

        public virtual IEnumerable<ISelectFieldOptionViewModel> GetOptionsForDisplay(IEnumerable<object> objectIds,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser)
            => GetAllByIds(objectIds, authenticatedUser)
                .Where(x => x is ISelectFieldOptionViewModel)
                .Select(x => new SelectFieldOptionViewModel(x as ISelectFieldOptionViewModel));

        public virtual IEnumerable<ISelectFieldOptionViewModel> GetOptionsForFilter(
            FilterRule rule,
            PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
            => GetAll(rule, authenticatedUser).Where(x => x is ISelectFieldOptionViewModel)
                .Select(x => new SelectFieldOptionViewModel(x as ISelectFieldOptionViewModel));


        protected virtual IEnumerable<OptionsFieldDefinitionViewModel> GetOptions(IEnumerable<TViewModel> result,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (result.Any())
            {
                return _entityFormFieldDefinitionsService
                    .GetDisplayFieldsDefinitionViewModel(result, GetServiceModelType(), authenticatedUser)
                    .Where(x => x.Options != null && x.Options.Any())
                    .Select(x => new OptionsFieldDefinitionViewModel(x));
            }
            else
            {
                return new List<OptionsFieldDefinitionViewModel>();
            }
        }

        public ListWithOptionsResponse<TViewModel> GetAllWithOptions(AuthenticatedUserViewModel authenticatedUser)
        {
            var result = this.GetAll(authenticatedUser);
            return new ListWithOptionsResponse<TViewModel>()
            {
                Result = result,
                Options = GetOptions(result, authenticatedUser)
            };
        }

        public ListWithOptionsResponse<TViewModel> GetAllWithOptions(FilterRule rule,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var result = this.GetAll(rule, authenticatedUser);
            return new ListWithOptionsResponse<TViewModel>()
            {
                Result = result,
                Options = GetOptions(result, authenticatedUser)
            };
        }

        public TViewModel GetByGuid(string guid, AuthenticatedUserViewModel authenticatedUser)
        {
            return MapToViewModel(_repository.GetByGuid(guid));
        }

        public PagedListWithOptionsResponse<TViewModel> GetPagedWithOptions(PagedListParameters pagingParams,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var result = this.GetPaged(pagingParams, authenticatedUser);
            return new PagedListWithOptionsResponse<TViewModel>()
            {
                Result = result,
                Options = GetOptions(result.List, authenticatedUser)
            };
        }


        public PagedListWithOptionsResponse<TViewModel> GetPagedWithOptions(FilterRule rule,
            PagedListParameters pagingParams, AuthenticatedUserViewModel authenticatedUser)
        {
            var result = this.GetPaged(rule, pagingParams, authenticatedUser);
            return new PagedListWithOptionsResponse<TViewModel>()
            {
                Result = result,
                Options = GetOptions(result.List, authenticatedUser)
            };
        }

        #endregion


        public virtual TViewModel Add(TViewModel value, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true)
        {
            var entity = MapToModel(value);
            CheckIfUserIsAuthorizedIfNotThrowException(OperationTypeEnum.ADD, entity, authenticatedUser);
            BeforeAdd(entity, value, authenticatedUser);
            _repository.Add(entity);
            MakeCommitIfNecessary(commit, authenticatedUser);
            AfterAdd(entity, value, authenticatedUser);
            // return MapToViewModel(entity);
            return GetById(VerifyEntityId(entity.GetEntityIdOrThrowException(GetIdPropertyName())), authenticatedUser);
        }

        public IEnumerable<TViewModel> BatchAdd(IEnumerable<TViewModel> values,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            var result = new List<TViewModel>();
            values.ForEach(x => result.Add(Add(x, authenticatedUser, false)));
            MakeCommitIfNecessary(commit, authenticatedUser);
            return result;
        }

        public virtual TViewModel Update(TViewModel value, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true)
        {
            var id = value?.GetEntityIdOrThrowException();
            var entity = MapToModel(value);
            CheckIfUserIsAuthorizedIfNotThrowException(OperationTypeEnum.UPDATE, entity, authenticatedUser);
            _repository.ChangeDetectChangesLazyLoading(true);
            var efEntity = _repository.GetById(VerifyEntityId(id), IncludesForGetOneRequestParams(authenticatedUser));
            var rawEfEntity = efEntity.ToJson().FromJson<TModel>();
            efEntity = Map(efEntity, entity);
            BeforeUpdate(efEntity, value, authenticatedUser);
            _repository.Update(efEntity);
            MakeCommitIfNecessary(commit, authenticatedUser);
            _repository.ChangeDetectChangesLazyLoading(false);
            AfterUpdate(rawEfEntity, efEntity, value, authenticatedUser);
            return MapToViewModel(entity);
        }

        protected virtual object VerifyEntityId(object id)
        {
            if (ReflectionHelper.IsNumericType(id.GetType()))
            {
                return id;
            }

            var asNumber = ParsingHelper.GetLong(id);
            if (id.ToString().EqualsIgnoreCase(asNumber.ToString()))
            {
                return asNumber;
            }
            else
            {
                return id;
            }
        }


        public TViewModel AddOrUpdate(TViewModel value, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true)
        {
            var id = value?.GetEntityIdOrThrowException();
            var idAsNumber = ParsingHelper.GetInt(id);
            if (IsNew(value))
            {
                return Add(value, authenticatedUser, commit);
            }
            else
            {
                return Update(value, authenticatedUser, commit);
            }
        }

        public IEnumerable<TViewModel> BatchUpdate(IEnumerable<TViewModel> values,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            var result = new List<TViewModel>();
            values.ForEach(x => result.Add(Update(x, authenticatedUser, false)));
            MakeCommitIfNecessary(commit, authenticatedUser);
            return result;
        }

        public virtual TViewModel Delete(object id, AuthenticatedUserViewModel authenticatedUser, bool commit = false)
        {
            var entity = _repository.GetById(VerifyEntityId(id));
            CheckIfUserIsAuthorizedIfNotThrowException(OperationTypeEnum.DELETE, entity, authenticatedUser);
            _repository.Delete(entity);
            MakeCommitIfNecessary(commit, authenticatedUser);
            AfterDelete(entity, authenticatedUser);
            return MapToViewModel(entity);
        }

        public IEnumerable<TViewModel> BatchDelete(IEnumerable<object> ids,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            var result = new List<TViewModel>();
            ids.ForEach(x => result.Add(Delete(x, authenticatedUser, false)));
            MakeCommitIfNecessary(commit, authenticatedUser);
            return result;
        }

        #endregion

        #region before_after_modifications

        protected virtual void UpdateObjectByAdditionalParams(TViewModel entity,
            Dictionary<string, string> additionalParams)
        {
            additionalParams.ForEach(x =>
            {
                var (key, value) = x;
                var currentValue = ReflectionHelper.GetPropValue(entity, key);
                var property = ReflectionHelper.GetPropertyByName(entity?.GetType(), key);
                if (property != null && ReflectionHelper.IsValueEmpty(
                    property?.PropertyType,
                    currentValue))
                {
                    ReflectionHelper.SetPropValue(entity, key, value);
                }
            });
        }

        protected virtual void BeforeAdd(TModel entity, TViewModel viewModel,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (entity.Guid == null)
            {
                entity.Guid = Guid.NewGuid();
            }

            if (entity is BaseEntity<long> be)
            {
                be.Id = 0;
            }

            ClearReferencedEntities(entity);
            ProcessDataAttributes(entity, authenticatedUser);
        }

        protected void ProcessDataAttributes(TModel entity, AuthenticatedUserViewModel authenticatedUser)
        {
            ReflectionHelper.GetPropertiesWithAttribute(entity, typeof(SetLoggedUserAttribte))
                .Where(x => x.GetValue(entity) == null)
                .ForEach(x => { x.SetValue(entity, authenticatedUser.Id); });
            ReflectionHelper.GetPropertiesWithAttribute(entity, typeof(SetDefaultValueAttribute))
                .ForEach(x =>
                {
                    var isEmpty = ReflectionHelper.IsValueEmpty(x.GetType(), x.GetValue(entity));
                    var attribute = x.GetCustomAttribute(typeof(SetDefaultValueAttribute)) as SetDefaultValueAttribute;
                    if (IsNew(entity))
                    {
                        x.SetValue(entity, ReflectionHelper.CreateDefaultInstance(x.GetType()));
                    }
                    else
                    {
                        if (attribute.Always || isEmpty)
                        {
                            x.SetValue(entity, ReflectionHelper.CreateDefaultInstance(x.GetType()));
                        }
                    }
                });
        }

        protected virtual void AfterAdd(TModel entity, TViewModel viewModel,
            AuthenticatedUserViewModel authenticatedUser)
        {
            ClearCache(entity);
        }


        protected virtual void BeforeUpdate(TModel entity, TViewModel viewModel,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (entity.Guid == null)
            {
                entity.Guid = Guid.NewGuid();
            }

            ClearReferencedEntities(entity);
            ProcessDataAttributes(entity, authenticatedUser);
        }

        protected virtual void AfterUpdate(TModel beforeUpdateEfEntity, TModel afterUpdateEfEntity,
            TViewModel viewModel, AuthenticatedUserViewModel authenticatedUser)
        {
            ClearCache(afterUpdateEfEntity);
        }

        protected virtual void AfterDelete(TModel entity, AuthenticatedUserViewModel authenticatedUser)
        {
            ClearCache(entity);
        }

        private void ClearReferencedEntities(TModel entity)
        {
            // DAFRA not needed because mapper not maps referenced entities inside
            //entity.GetType().GetProperties()
            //  .Where(x => x.Name.ToLower().EndsWith("id"))
            //  .Where(x => x.GetCustomAttribute(typeof(NotMappedAttribute)) == null)
            //  .ForEach(prop =>
            //  {
            //    ReflectionHelper.SetPropValue(entity, prop.Name.Replace("Id", ""), null);
            //  });
        }

        #endregion

        #region mappings

        protected virtual TViewModel MapToViewModel(TModel model)
        {
            try
            {
                var result = model?.Map<TViewModel>();
                var selectionViewModel = result as ISelectFieldOptionViewModel;
                if (selectionViewModel != null)
                {
                    var objDict = result?.ToDictionary().Where(x => !x.Key.ToLower().Contains("optionAdditionalInfo"));
                    if (objDict != null)
                    {
                        objDict.ForEach(x =>
                            selectionViewModel.OptionAdditionalInfo.TryAdd(x.Key.ToCamelCase(), x.Value?.ToString()));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while mapping to view model {model}", ex);
                return new TViewModel();
            }
        }

        protected virtual TModel MapToModel(TViewModel viewModel)
        {
            try
            {
                return viewModel?.Map<TModel>();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while mapping to  model {viewModel}", ex);
                return new TModel();
            }
        }

        protected virtual TViewModel CopyViewModel(TViewModel viewModel)
        {
            var copy = viewModel?.Map<TViewModel>();
            if (copy != null)
            {
                copy.SetPropertyValue("Id", 0);
            }

            return copy;
        }

        protected virtual TModel Map(TModel originalEntity, TModel newEntity)
        {
            return newEntity.Map(originalEntity);
        }

        protected virtual PagedList<TViewModel> MapToPagedList(PagedList<TModel> pagedList)
        {
            return new PagedList<TViewModel>(
                pagedList.Count,
                MapToViewModelList(pagedList.List),
                pagedList.Index,
                pagedList.Size);
        }

        protected virtual IEnumerable<TViewModel> MapToViewModelList(IEnumerable<TModel> list)
        {
            return list.Select(x => MapToViewModel(x));
        }

        #endregion

        #region filtering

        protected virtual Expression<Func<TModel, bool>> FilterForGetRequest(
            AuthenticatedUserViewModel authenticatedUser) =>
            (entity) => !entity.IsDeleted;

        protected virtual void CheckIfUserIsAuthorizedIfNotThrowException(OperationTypeEnum operationType,
            TModel entity, AuthenticatedUserViewModel authenticatedUser)
        {
            // TODO
        }

        protected Expression<Func<TModel, bool>> BuildGetAllExpressionForRule(FilterRule rule,
            AuthenticatedUserViewModel authenticatedUser)
        {
            if (rule == null || rule.IsEmpty())
            {
                return FilterForGetRequest(authenticatedUser);
            }
            else
            {
                return ExpressionHelper.And(FilterForGetRequest(authenticatedUser),
                    ProcessUserFilterRole(rule)
                        .BuildExpressionLambda<TModel>(new BuildExpressionOptions() { }, out string query));
            }
        }

        protected virtual FilterRule ProcessUserFilterRole(FilterRule rule)
        {
            var rules = new Dictionary<string, FilterRule>();
            var rawRules = new List<FilterRule>();
            var condition = rule.Condition ?? "";

            rule.Rules?.ForEach(x =>
            {
                // for or rule there can be many the same fields
                if (!string.IsNullOrEmpty(x.Field) && rule.Condition != "or")
                {
                    rules[x.Field] = x;
                }
                else
                {
                    rawRules.Add(x);
                }
            });
            rule.Rules = rawRules;
            rule.Rules.AddRange(rules.Values.ToList());
            rule.Rules.ForEach(x => ProcessUserFilterRole(x));

            return rule;
        }

        #endregion

        #region metadata

        protected virtual IEnumerable<Order> GetDefaultOrder()
        {
            return new List<Order>() {new Order() {IsAscending = true, Property = "Id"}};
        }

        public virtual bool CanDisplayField(object editedObject, PropertyInfo propertyInfo,
            EntityFormFieldDefinitionFilterType type, AuthenticatedUserViewModel authenticatedUser)
        {
            return true;
        }

        public virtual bool IsEditable(object editedObject, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return true;
        }

        public Type GetServiceModelType()
        {
            return typeof(TViewModel);
        }

        public Type GetModelType()
        {
            return typeof(TModel);
        }

        public IEnumerable<DisplayFieldDefinitionViewModel> GetExportFieldsDefinitionModel(
            IEnumerable<TViewModel> objects, AuthenticatedUserViewModel authenticatedUser)
        {
            return _entityFormFieldDefinitionsService
                .GetExportFieldsDefinitionViewModel(GetServiceModelType(), objects, authenticatedUser);
        }

        public IEnumerable<DisplayFieldDefinitionViewModel> GetAllFieldsDefinitionModel(
            IEnumerable<TViewModel> objects, AuthenticatedUserViewModel authenticatedUser)
        {
            return _entityFormFieldDefinitionsService
                .GetAllFieldsDefinitionViewModel(GetServiceModelType(), objects, authenticatedUser);
        }

        public virtual IEnumerable<string> GetVisibleFields(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual IEnumerable<string> GetExcludeFields(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual IEnumerable<string> GetFilterableFields(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual IEnumerable<string> GetExportableFields(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual IEnumerable<FieldOrderViewModel> GetFieldsOrder(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual IEnumerable<UserDefinedFormGroupViewModel> GetFormGroups(
            AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }


        public virtual List<FormGroupViewModel> ValidateFormGroups(List<FormGroupViewModel> formModel,
            object editedObject, AuthenticatedUserViewModel authenticatedUser)
        {
            return formModel;
        }

        #endregion

        #region utils

        protected virtual void ClearCache(TModel entity)
        {
        }

        protected void MakeCommitIfNecessary(bool commit, AuthenticatedUserViewModel authenticatedUser)
        {
            if (commit)
            {
                _repository.SaveChanges(authenticatedUser);
            }
        }

        #endregion

        protected Expression<Func<TModel, object>>[] IncludesForGetListRequestParams(
            AuthenticatedUserViewModel authenticatedUser) => IncludesForGetListRequest(authenticatedUser)?.ToArray();

        protected Expression<Func<TModel, object>>[] IncludesForGetOneRequestParams(
            AuthenticatedUserViewModel authenticatedUser) => IncludesForGetOneRequest(authenticatedUser)?.ToArray();

        protected virtual IEnumerable<Expression<Func<TModel, object>>> IncludesForGetListRequest(
            AuthenticatedUserViewModel authenticatedUser) => new List<Expression<Func<TModel, object>>>();

        protected virtual IEnumerable<Expression<Func<TModel, object>>> IncludesForGetOneRequest(
            AuthenticatedUserViewModel authenticatedUser) => new List<Expression<Func<TModel, object>>>();

        public IEnumerable<TViewModel> BulkCreate(IEnumerable<TViewModel> objectsToCreate,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            return objectsToCreate.Select(x => { return Add(x, authenticatedUser, commit); });
        }

        public IEnumerable<TViewModel> BulkUpdate(IEnumerable<object> objectsToUpdateIds, TViewModel value,
            AuthenticatedUserViewModel authenticatedUser)
        {
            var objectsToUpdate = GetAllByIds(objectsToUpdateIds, authenticatedUser);
            return objectsToUpdate.Select(x =>
            {
                ReflectionHelper.CopyProperties(value, x, true, "Id", "Guid");
                return Update(x, authenticatedUser);
            });
        }

        public IEnumerable<TViewModel> BulkAddOrUpdate(IEnumerable<TViewModel> list,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            return list.Select(x => { return AddOrUpdate(x, authenticatedUser, commit); });
        }

        public virtual FormGroupLayoutComponent? GetFormGroupLayoutType(AuthenticatedUserViewModel authenticatedUser)
        {
            return null;
        }

        public virtual bool CanAddInline(object editedObject, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return true;
        }

        #region raw

        IEnumerable<object> IRawCrudService.GetAll(AuthenticatedUserViewModel authenticatedUser)
        {
            return GetAll(authenticatedUser);
        }

        IEnumerable<object> IRawCrudService.GetAllByIds(IEnumerable<object> ids,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return GetAllByIds(ids, authenticatedUser);
        }

        IEnumerable<object> IRawCrudService.GetAll(FilterRule rule, AuthenticatedUserViewModel authenticatedUser)
        {
            return GetAll(rule, authenticatedUser);
        }


        object IRawCrudService.GetById(object id, AuthenticatedUserViewModel authenticatedUser)
        {
            return GetById(id, authenticatedUser);
        }

        object IRawCrudService.GetByGuid(string guid, AuthenticatedUserViewModel authenticatedUser)
        {
            return GetByGuid(guid, authenticatedUser);
        }

        public object Add(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            return Add(value as TViewModel, authenticatedUser);
        }

        public object Update(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            return Update(value as TViewModel, authenticatedUser);
        }

        public object AddOrUpdate(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true)
        {
            return AddOrUpdate(value as TViewModel, authenticatedUser);
        }

        object IRawCrudService.Delete(object id, AuthenticatedUserViewModel authenticatedUser, bool commit)
        {
            return Delete(id, authenticatedUser);
        }


        public FormModel<object> GetWithEditDataById(object id, Dictionary<string, string> additionalParams,
            AuthenticatedUserViewModel authenticatedUser)
        {
            return GetWithEditDataById(id, additionalParams, authenticatedUser);
        }

        protected bool IsNew(object value)
        {
            var id = value?.GetEntityIdOrThrowException();
            var idAsNumber = ParsingHelper.GetInt(id);
            return string.IsNullOrEmpty(id?.ToString()) || idAsNumber < 1;
        }

        public virtual IEnumerable<ISelectFieldOptionViewModel> Search(string term,
            string modelType,
            string fieldName,
            AuthenticatedUserViewModel authenticatedUser,
            int maxResults = 30)
        {
            var type = _repository.GetRepositoryModel();
            var stringProperties =
                type.GetProperties().Where(x => x.PropertyType == typeof(string)).Select(x => x.Name);
            FilterRule rule = null;
            if (!string.IsNullOrEmpty(term))
            {
                rule = QueryBuilderHelper.BuildFilterRuleForManyFields(type, QueryConditionEnum.OR,
                    QueryOperatorEnum.Like, term, stringProperties.ToArray());
            }

            if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(modelType))
            {
                var ruleByModelType = GetRuleForSearchByModelType(modelType, fieldName);
                if (ruleByModelType != null)
                {
                    if (rule == null)
                    {
                        rule = ruleByModelType;
                    }
                    else
                    {
                        rule = new FilterRule()
                        {
                            Condition = "and",
                            Rules = new List<FilterRule>()
                            {
                                rule,
                                ruleByModelType
                            }
                        };
                    }
                }
            }

            return GetPaged(rule,
                    new PagedListParameters()
                        {Page = new Page() {Size = maxResults}, Order = GetDefaultOrder().FirstOrDefault()},
                    authenticatedUser)
                .List.Where(x => x is ISelectFieldOptionViewModel)
                .Select(x => new SelectFieldOptionViewModel(x as ISelectFieldOptionViewModel));
        }

        protected virtual FilterRule GetRuleForSearchByModelType(string modelType, string fieldName)
        {
            return null;
        }

        protected virtual PagedListParameters VerifyPagedParams(PagedListParameters pagedParams)
        {
            if (string.IsNullOrEmpty(pagedParams?.Order?.Property))
            {
                pagedParams.Order = GetDefaultOrder().FirstOrDefault();
            }

            return pagedParams;
        }

        #endregion
    }
}