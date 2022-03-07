using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEFBDAACommons.Shared.Helpers;
using System.Collections.Generic;

namespace NEFBDAACommons.Database.Converters
{
    public class StoreStringListAsStringConverter<T> : ValueConverter<T, string>
        where T : class
    {


        public StoreStringListAsStringConverter(ConverterMappingHints hints = default) :
       base(v => Serialize(v), v => Deserialize(v), hints)
        { }

        private static T Deserialize(string value)
        {
            return ExporterHelper.GetListOfStringsFromString(value, CommonsConstants.DEFAULT_STORE_LIST_AS_STRING_SEPARATOR) as T;
        }

        private static string Serialize(T v)
        {
            return ExporterHelper.StoreListAsString(v as IEnumerable<string>, CommonsConstants.DEFAULT_STORE_LIST_AS_STRING_SEPARATOR);
        }
    }

}
