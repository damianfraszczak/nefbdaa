using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.DynamicForms.Services;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.DynamicQuery;
using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Database.Services
{
    public interface IRawCrudService : IEntityListProviderService, IEntityFormFieldDefinitionPreparatorService
    {
        Type GetServiceModelType();

        IEnumerable<object> GetAll(AuthenticatedUserViewModel authenticatedUser);
        IEnumerable<object> GetAllByIds(IEnumerable<object> ids, AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<object> GetAll(FilterRule rule, AuthenticatedUserViewModel authenticatedUser);
        // TODO Paged raw
        //PagedList<object> GetPaged(PagedListParameters pagingParams, AuthenticatedUserViewModel authenticatedUser);
        //PagedList<object> GetPaged(FilterRule rule, PagedListParameters pagingParams, AuthenticatedUserViewModel authenticatedUser);

        object GetById(object id, AuthenticatedUserViewModel authenticatedUser);
        object GetByGuid(string guid, AuthenticatedUserViewModel authenticatedUser);
        object Add(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        object Update(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);
        object AddOrUpdate(object value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);
        object Delete(object id, AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        FormModel<object> GetWithEditDataById(object id, Dictionary<string, string> additionalParams,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<ISelectFieldOptionViewModel> Search(string term,
            string modelType,
            string fieldName,
            AuthenticatedUserViewModel authenticatedUser,
            int maxResults = 30);
    }

    public interface ICrudService<TViewModel> : IRawCrudService
        where TViewModel : class
    {
        new IEnumerable<TViewModel> GetAll(AuthenticatedUserViewModel authenticatedUser);
        new IEnumerable<TViewModel> GetAllByIds(IEnumerable<object> ids, AuthenticatedUserViewModel authenticatedUser);
        new IEnumerable<TViewModel> GetAll(FilterRule rule, AuthenticatedUserViewModel authenticatedUser);
        PagedList<TViewModel> GetPaged(PagedListParameters pagingParams, AuthenticatedUserViewModel authenticatedUser);

        PagedList<TViewModel> GetPaged(FilterRule rule, PagedListParameters pagingParams,
            AuthenticatedUserViewModel authenticatedUser);

        new TViewModel GetById(object id, AuthenticatedUserViewModel authenticatedUser);
        new TViewModel GetByGuid(string guid, AuthenticatedUserViewModel authenticatedUser);
        TViewModel GetByRule(FilterRule rule, AuthenticatedUserViewModel authenticatedUser);
        TViewModel Add(TViewModel value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        IEnumerable<TViewModel> BatchAdd(IEnumerable<TViewModel> values, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true);

        TViewModel Update(TViewModel value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);
        TViewModel AddOrUpdate(TViewModel value, AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        IEnumerable<TViewModel> BatchUpdate(IEnumerable<TViewModel> values,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        IEnumerable<TViewModel> BulkAddOrUpdate(IEnumerable<TViewModel> list,
            AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        IEnumerable<TViewModel> BulkCreate(IEnumerable<TViewModel> list, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true);

        new TViewModel Delete(object id, AuthenticatedUserViewModel authenticatedUser, bool commit = true);

        IEnumerable<TViewModel> BatchDelete(IEnumerable<object> ids, AuthenticatedUserViewModel authenticatedUser,
            bool commit = true);

        FormModel<TViewModel> GetWithEditDataById<TID>(TID id, Dictionary<string, string> additionalParams,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<TViewModel> BulkUpdate(IEnumerable<object> objectsToUpdate, TViewModel value,
            AuthenticatedUserViewModel authenticatedUser);

        ListWithOptionsResponse<TViewModel> GetAllWithOptions(AuthenticatedUserViewModel authenticatedUser);

        ListWithOptionsResponse<TViewModel> GetAllWithOptions(FilterRule rule,
            AuthenticatedUserViewModel authenticatedUser);

        PagedListWithOptionsResponse<TViewModel> GetPagedWithOptions(PagedListParameters pagingParams,
            AuthenticatedUserViewModel authenticatedUser);

        PagedListWithOptionsResponse<TViewModel> GetPagedWithOptions(FilterRule rule, PagedListParameters pagingParams,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<DisplayFieldDefinitionViewModel> GetAllFieldsDefinitionModel(IEnumerable<TViewModel> objects,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<DisplayFieldDefinitionViewModel> GetExportFieldsDefinitionModel(IEnumerable<TViewModel> objects,
            AuthenticatedUserViewModel authenticatedUser);
    }
}