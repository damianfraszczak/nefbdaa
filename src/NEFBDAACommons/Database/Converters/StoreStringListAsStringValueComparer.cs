using Microsoft.EntityFrameworkCore.ChangeTracking;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Database.Converters
{
    public class StoreStringListAsStringValueComparer<T> : ValueComparer<T>
    {

        private static string ToString(T instance)
        {
            return ExporterHelper.StoreListAsString(instance as IEnumerable<string>);
        }
        private static T DoGetSnapshot(T instance)
        {

            if (instance is ICloneable cloneable)
                return (T)cloneable.Clone();

            var result = (T)ExporterHelper.GetListOfStringsFromString(instance?.ToString());
            return result;

        }

        private static int DoGetHashCode(T instance)
        {

            if (instance is IEquatable<T>)
                return instance.GetHashCode();

            return ToString(instance).GetHashCode();

        }

        private static bool DoEquals(T left, T right)
        {

            if (left is IEquatable<T> equatable)
                return equatable.Equals(right);

            var result = ToString(left).Equals(ToString(right));
            return result;

        }

        public StoreStringListAsStringValueComparer() : base(
          (t1, t2) => DoEquals(t1, t2),
          t => DoGetHashCode(t),
          t => DoGetSnapshot(t))
        {
        }
    }

}
