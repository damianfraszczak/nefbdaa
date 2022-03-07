using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class EnumHelper
    {
        public static T GetValueAttribute<T>(Type type, string enumValue) where T : Attribute
        {
            type = ReflectionHelper.GetNullableTypeIfIs(type);
            if (!type.IsEnum)
            {
                throw new ArgumentException($"EnumerationValue must be of Enum type - {type}", "type");
            }

            if (string.IsNullOrEmpty(enumValue)) return null;
            var memberInfo = type.GetMember(enumValue);
            if (memberInfo == null || memberInfo.Length <= 0) return null;
            var attrs = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((T) attrs[0]);
            }

            return null;
        }

        public static string GetDescription<T>(T enumerationValue)
        {
            var type = enumerationValue.GetType();
            var descriptionAttribute = GetValueAttribute<DescriptionAttribute>(type, enumerationValue.ToString());
            return descriptionAttribute != null ? descriptionAttribute.Description : enumerationValue.ToString();
        }

        public static Type GetEnumTypeByFullName(string enumName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(enumName);
                if (type == null)
                    continue;
                if (type.IsEnum)
                    return type;
            }

            return null;
        }

        public static Type GetEnumTypeBySimpleName(string enumName)
        {
            var allEnums = ReflectionHelper.GetTypesAssignableTo(typeof(Enum));
            return allEnums.FirstOrDefault(x => x.Name.Equals(enumName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<T> GetAllAsList<T>(Type enumerationType)
        {
            return (T[]) Enum.GetValues(enumerationType);
        }

        public static IDictionary<string, string> GetAll<T>(T enumerationType) where T : Type
        {
            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<string, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var enumValue = Enum.ToObject(enumerationType, value);
                var description = GetDescription(enumValue);
                dictionary.Add(Enum.GetName(enumerationType, value), description);
            }

            return dictionary;
        }

        public static T GetValueForString<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }

        public static T GetValueForInt<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T) Enum.ToObject(enumType, intValue);
        }
    }
}