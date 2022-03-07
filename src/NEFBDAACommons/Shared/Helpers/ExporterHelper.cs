using NEFBDAACommons.Database.Models;
using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Models;
using NEFBDAACommons.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class ExporterHelper
    {
        public static string ExportData<T>(DataExportTypeEnum type, IEnumerable<T> dataToExport,
            IEnumerable<DisplayFieldDefinitionViewModel> fieldsDefinitions)
        {
            return CreateCSV(dataToExport, fieldsDefinitions);
        }

        public static string CreateCSV<T>(IEnumerable<T> list,
            IEnumerable<DisplayFieldDefinitionViewModel> fieldDefinitions, string separator = ",")
        {
            fieldDefinitions = fieldDefinitions.Where(x => x.Exportable);
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(separator, fieldDefinitions.Select(x => x.Placeholder)));
            list.ForEach(item =>
            {
                builder.AppendLine(string.Join(separator, fieldDefinitions.Select(x => GetItemValue(item, x))));
            });
            return builder.ToString();
        }

        private static string GetItemValue<T>(T item, DisplayFieldDefinitionViewModel fieldDefinition)
        {
            var property = ReflectionHelper.GetPropertyByName(typeof(T), fieldDefinition.ParameterName);
            var value = property
                ?.GetValue(item);
            try
            {
                if (value != null)
                {
                    switch (fieldDefinition.InputType)
                    {
                        case InputType.Password:
                            return "";
                        case InputType.Select:
                        case InputType.Radio:
                            return fieldDefinition.Options
                                .FirstOrDefault(x => x.OptionId == value.ToString())
                                ?.OptionText;

                        case InputType.SelectMultiple:
                            return string.Join(',', fieldDefinition.Options
                                .Where(x => x.OptionId == value.ToString())
                                .Select(x => x.OptionText));
                        case InputType.Date:
                            return FormattingHelper.GetFormattedDate(value as DateTime?);
                        case InputType.Time:
                            return FormattingHelper.GetFormattedTime(value as DateTime?);
                        case InputType.DateTime:
                            return FormattingHelper.GetFormattedDateTime(value as DateTime?);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(typeof(ExporterHelper),
                    $"Error with exportuing value {property.Name} {value}", ex);
            }

            return value?.ToString();
        }

        public static string CreateCSV<T>(IEnumerable<T> list, string separator = ";")
        {
            return CreateCSV(list, new List<string>(), separator);
        }

        public static string CreateCSV<T>(IEnumerable<T> list, List<string> propsToSkip, string separator = ";")
        {
            var builder = new StringBuilder();
            var properties = typeof(T).GetProperties().Where(x => !propsToSkip.Contains(x.Name)).ToArray();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                builder.Append(properties[i].Name + separator);
            }

            var lastPropName = properties[properties.Length - 1].Name;
            builder.AppendLine(lastPropName);
            foreach (var item in list)
            {
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    builder.Append(prop.GetValue(item) + separator);
                }

                var lastProp = properties[properties.Length - 1];
                builder.AppendLine(lastProp.GetValue(item) + "");
            }

            return builder.ToString();
        }


        public static string StoreListAsString(IEnumerable<object> list, string separator = ",")
        {
            if (list == null) return null;
            return string.Join(separator, list.Select(x => x.ToString()).ToList().Distinct());
        }

        public static IEnumerable<string> GetListOfStringsFromString(string val, string separator = ",")
        {
            if (string.IsNullOrEmpty(val)) return null;
            return val.Split(separator);
        }
    }
}