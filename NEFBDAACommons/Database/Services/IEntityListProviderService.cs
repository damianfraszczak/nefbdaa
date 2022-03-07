using NEFBDAACommons.DI.Attributes;
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
using NEFBDAACommons.Web.Models;

namespace NEFBDAACommons.Database.Services
{
    public interface IEntityListProviderService
    {
        IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEdit(OptionsForEdit optionsForEdit, AuthenticatedUserViewModel authenticatedUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editedObject">Other object</param>
        /// <param name="propertyInfo"></param>
        /// <param name="rule"></param>
        /// <param name="authenticatedUser"></param>
        /// <returns></returns>
        /// 
        IEnumerable<ISelectFieldOptionViewModel> GetOptionsForEdit(object editedObject, PropertyInfo propertyInfo,
            FilterRule rule,
            AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<ISelectFieldOptionViewModel> GetOptionsForDisplay(IEnumerable<object> objectsIds,
            PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser);

        IEnumerable<ISelectFieldOptionViewModel> GetOptionsForFilter(FilterRule rule, PropertyInfo propertyInfo,
            AuthenticatedUserViewModel authenticatedUser);

        FilterRule GetRuleToFilter(object objectOrList, PropertyInfo propertyInfo, AuthenticatedUserViewModel authenticatedUser);
    }

    public interface IServicesProviderLocator
    {
        IEntityListProviderService GetListProviderService(Type objectType);
        IEntityFormFieldDefinitionPreparatorService GetEntityFormFieldDefinitionPreparatorService(Type objectType);
        T GetServiceFromProvider<T>(Type serviceType) where T : class;
    }

    // [ScopeService]
    public class ServicesProviderLocator : AbstractService, IServicesProviderLocator
    {
        private readonly IServiceProvider _provider;

        public ServicesProviderLocator(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public IEntityFormFieldDefinitionPreparatorService GetEntityFormFieldDefinitionPreparatorService(
            Type objectType)
        {
            return GetService<IEntityFormFieldDefinitionPreparatorService>(objectType);
        }

        public IEntityListProviderService GetListProviderService(Type objectType)
        {
            return GetService<IEntityListProviderService>(objectType);
        }

        public T GetServiceFromProvider<T>(Type serviceType) where T : class
        {
            return _provider.GetService(serviceType) as T;
        }

        private T GetService<T>(Type associationType) where T : class
        {
            try
            {
                Type serviceType = typeof(T);
                var types = ReflectionHelper.GetTypesAssignableTo(serviceType, false).ToList();
                // TODO napisac to lepiej
                var servicesInterfaces = types
                    .Where(x => x.GetInterfaces().Any())
                    .ToDictionary(x => x, x => ReflectionHelper.GetParentTypes(x).Union(new List<Type>() {x}));
                // x => x.GenericTypeArguments.Any(genericEntityType => genericEntityType == associationType)));
                var serviceInterfaces = servicesInterfaces
                    .Where(pair => pair.Value.Any(x =>
                        x.GenericTypeArguments.Any(genericEntityType =>
                            associationType.IsAssignableFrom(genericEntityType))))
                    .Select(x => x.Key);

                var serviceInterface = serviceInterfaces.FirstOrDefault();
                if (serviceInterfaces.Count() > 1)
                {
                    serviceInterface = serviceInterfaces.FirstOrDefault(x =>
                        x.GenericTypeArguments.Any(genericEntityType => associationType.Equals(genericEntityType)));
                    if (serviceInterface == null)
                    {
                        serviceInterface = serviceInterfaces.FirstOrDefault(x =>
                            ReflectionHelper.GetParentTypes(x).SelectMany(y => y.GenericTypeArguments)
                                .Any(genericEntityType => associationType.Equals(genericEntityType)));
                        if (serviceInterface == null)
                        {
                            serviceInterface = HandleMultipleFoundInstances(serviceType, serviceInterfaces);
                            // TODO improve in future
                            // throw new ConfigException($"There are more than one service which can be used for this context {associationType.Name}");
                        }
                    }
                }

                if (serviceInterface != null)
                {
                    return GetServiceFromProvider<T>(serviceInterface);
                }
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(typeof(ServicesProviderLocator), $"Error create service {associationType}",
                    ex);
            }

            return null;
        }

        protected virtual Type HandleMultipleFoundInstances(Type serviceType, IEnumerable<Type> serviceInterfaces)
        {
            ApplicationLogger.LogDebug(typeof(ServicesProviderLocator),
                $"Multiple interfaces implementation found for {serviceType}. It found {string.Join(",", serviceInterfaces.Select(x => x.Name))}. returns first one");
            return serviceInterfaces.FirstOrDefault();
        }
    }
}