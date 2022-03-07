using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Shared.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using NEFBDAACommons.Shared.Extensions;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class ReflectionHelper
    {
        public static object InvokeMethod(object source, string methodName, params object[] args)
        {
            MethodInfo methodInfo = source.GetType().GetMethod(methodName);

            if (methodInfo != null)
            {
                var methodInfoParams = methodInfo.GetParameters();
                return methodInfo.Invoke(methodInfo, args);
            }
            else
            {
                throw new Exception($"Method {methodName} doesnt exists in {source.GetType()}");
            }
        }

        public static object GetPropValue(object source, string propertyName)
        {
            return GetPropertyByName(source, propertyName)?.GetValue(source);
        }

        public static PropertyInfo GetPropertyByName(object source, string propertyName)
        {
            return GetPropertyByName(source.GetType(), propertyName);
        }

        public static PropertyInfo GetPropertyByName(Type type, string propertyName)
        {
            PropertyInfo property = null;
            var propertyList = propertyName?.Split('.');
            propertyList?.ForEach(innerProperty =>
            {
                property = type?.GetRuntimeProperties()
                    .FirstOrDefault(p =>
                        string.Equals(p.Name, innerProperty, StringComparison.InvariantCultureIgnoreCase));
                type = GetAnyElementType(property?.PropertyType);
            });

            return property;
        }

        public static IEnumerable<Type> GetParentTypes(Type type)
        {
            // is there any base type?
            if (type == null)
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }

        public static IEnumerable<Type> GetTypesWithAttribute(Type attribute)
        {
            foreach (Type type in GetAllTypes(false))
            {
                if (type.GetCustomAttributes(attribute, true).Length > 0)
                {
                    yield return type;
                }
            }
        }


        public static bool SetPropValue(object source, string propertyName, object propertyVal)
        {
            try
            {
                var propertyInfo = GetPropertyByName(source, propertyName);
                if (propertyInfo != null)
                {
                    var propertyType = propertyInfo.PropertyType;
                    var targetType = IsNullableType(propertyType)
                        ? Nullable.GetUnderlyingType(propertyType)
                        : propertyType;
                    if (targetType == typeof(Guid))
                    {
                        propertyVal = Guid.Parse(propertyVal.ToString());
                    }


                    propertyVal = Convert.ChangeType(propertyVal, targetType);
                    propertyInfo.SetValue(source, propertyVal, null);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(typeof(ReflectionHelper),
                    $"Error with setting prop value for {source.GetType()} {propertyName} {propertyVal}", ex);
            }

            return false;
        }


        public static IEnumerable<Type> GetTypesAssignableTo(Type assignableType, bool onlyIntefaces = false)
        {
            foreach (Type type in GetAllTypes(false).Where(x => onlyIntefaces ? x.IsInterface : true))
            {
                if (assignableType.IsAssignableFrom(type))
                {
                    yield return type;
                }
            }
        }


        public static List<Type> GetSubclassesOf(this Type type, bool excludeSystemTypes)
        {
            return GetAllTypes(excludeSystemTypes)
                .Where(t => type.IsAssignableFrom(t))
                .ToList();
        }

        public static ISet<Type> GetAllTypes(bool excludeSystemTypes)
        {
            var list = new HashSet<Type>();
            IEnumerator enumerator = Thread.GetDomain().GetAssemblies().GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    Type[] types = ((Assembly) enumerator.Current).GetTypes();
                    if (!excludeSystemTypes || (excludeSystemTypes &&
                                                !((Assembly) enumerator.Current).FullName.StartsWith("System.")))
                    {
                        IEnumerator enumerator2 = types.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            Type current = (Type) enumerator2.Current;
                            list.Add(current);
                        }
                    }
                }
                catch
                {
                    // ignore all exceptions
                }
            }

            return list;
        }

        public static bool IsNumericType(Type type)
        {
            if (type.IsEnum)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                    return true;
                default:
                    return IsLikeIntegerType(type);
            }
        }

        public static bool IsLikeIntegerType(Type type)
        {
            if (type.IsEnum)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsLikeLong(Type type)
        {
            if (type.IsEnum)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int64:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBooleanType(Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }

        public static bool IsStringType(Type type)
        {
            return type == typeof(string);
        }

        public static bool IsDateTimeType(Type type)
        {
            return type == typeof(DateTime) || type == typeof(DateTime?);
        }

        public static void CopyProperties(object source, object destination, bool ignoreEmpty = false,
            params string[] ignoredFields)
        {
            // Iterate the Properties of the destination instance and  
            // populate them from their source counterparts  
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                if (!destinationPi.CanWrite || ignoredFields.Any(x => string.Equals(x, destinationPi.Name)))
                {
                    continue;
                }

                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                if (sourcePi != null)
                {
                    var value = sourcePi.GetValue(source, null);
                    if (!ignoreEmpty || !IsValueEmpty(sourcePi.PropertyType, value))
                    {
                        destinationPi.SetValue(destination, value);
                    }
                }
            }
        }

        public static bool IsValueEmpty(Type propertyType, object value)
        {
            var defString = ParsingHelper.GetNotNullString(value);
            if (string.IsNullOrEmpty(defString))
            {
                return true;
            }

            if (IsLikeIntegerType(propertyType) && ParsingHelper.GetLong(defString) == 0)
            {
                return true;
            }

            if (IsNumericType(propertyType) && ParsingHelper.GetDouble(defString) == 0.0)
            {
                return true;
            }

            if (propertyType == typeof(Guid) && defString == "00000000-0000-0000-0000-000000000000")
            {
                return true;
            }

            return false;
        }

        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static T CreateInstance<T>(Type objectType, params object[] ctorParams) where T : class
        {
            if (ctorParams.Any())
            {
                return Activator.CreateInstance(objectType, ctorParams) as T;
            }
            else
            {
                return Activator.CreateInstance(objectType) as T;
            }
        }

        public static bool IsEnumType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            var nullableEnum = GetNullableTypeIfIs(type);
            return nullableEnum.IsEnum;
        }

        public static Type GetNullableTypeIfIs(Type type)
        {
            if (type == null)
            {
                return null;
            }

            var nullable = Nullable.GetUnderlyingType(type);
            return nullable != null ? nullable : type;
        }

        public static bool IsEnumerationType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return typeof(Enumeration).IsAssignableFrom(type);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(object entity, Type attributeType)
        {
            return entity.GetType().GetProperties().Where(x => x.GetCustomAttribute(attributeType) != null);
        }

        public static object CreateDefaultInstance(Type type)
        {
            if (type == typeof(DateTime))
            {
                return DateTime.Now;
            }
            else
            {
                return CreateInstance<object>(type);
            }
        }

        public static bool IsEnumerableOrArrayType(object objectOrList)
        {
            var type = objectOrList?.GetType();
            if (type != null && type != typeof(string))
            {
                return typeof(IEnumerable).IsAssignableFrom(type) || type.IsArray;
            }
            else
            {
                return false;
            }
        }

        public static Type GetAnyElementType(Type type)
        {
            if (type == null)
            {
                return null;
            }

            type = GetNullableTypeIfIs(type);

            // Type is Array
            // short-circuit if you expect lots of arrays 
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces()
                .Where(t => t.IsGenericType &&
                            t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
            return enumType ?? type;
        }

        public static IEnumerable<object> GetAsEnumerable(object val)
        {
            List<object> list = new List<object>();
            if (IsEnumerableOrArrayType(val))
            {
                var enumerator = ((IEnumerable) val).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                }
            }

            return list;
        }
    }
}