using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEFBDAACommons.Database.Attributes;
using NEFBDAACommons.Database.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Database.Extensions
{
    public static class CustomConvertersExtensions
    {
        public static void AddStoreEnumAsString(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.BaseType == typeof(Enum))
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;
                        property.SetValueConverter(converter);
                    }
                }
            }
        }

        public static void AddStoreStringListAsStringFields(this ModelBuilder modelBuilder,
            bool skipConventionalEntities = true)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var typeBase = typeof(TypeBase);
                if (skipConventionalEntities)
                {
                    var typeConfigurationSource = typeBase
                        .GetField("_configurationSource", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.GetValue(entityType)?.ToString();
                    if (Enum.TryParse(typeConfigurationSource, out ConfigurationSource typeSource) &&
                        typeSource == ConfigurationSource.Convention) continue;
                }

                var ignoredMembers =
                    typeBase.GetField("_ignoredMembers", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.GetValue(entityType) as Dictionary<string, ConfigurationSource>;

                bool NotIgnored(PropertyInfo property) =>
                    property != null && !ignoredMembers.ContainsKey(property.Name) &&
                    !property.CustomAttributes.Any(a => a.AttributeType == typeof(NotMappedAttribute));

                foreach (var clrProperty in entityType.ClrType.GetProperties()
                    .Where(x => NotIgnored(x) && HasStoreStringListAsStringAttribute(x)))
                {
                    var property = modelBuilder.Entity(entityType.ClrType)
                        .Property(clrProperty.PropertyType, clrProperty.Name);
                    var modelType = clrProperty.PropertyType;

                    var converterType = typeof(StoreStringListAsStringConverter<>).MakeGenericType(modelType);
                    var converter = (ValueConverter) Activator.CreateInstance(converterType, new object[] {null});
                    property.Metadata.SetValueConverter(converter);

                    var valueComparer = typeof(StoreStringListAsStringValueComparer<>).MakeGenericType(modelType);
                    property.Metadata.SetValueComparer(
                        (ValueComparer) Activator.CreateInstance(valueComparer, new object[0]));
                }
            }
        }

        private static bool HasStoreStringListAsStringAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo != null &&
                   propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(StoreAsStringAttribute));
        }
    }
}