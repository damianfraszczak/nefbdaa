using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class EnumeratorExtensions
    {

        public static double AverageOrDefault(this IEnumerable<double> enumerable)
        {
            return enumerable.DefaultIfEmpty().Average();
        }

        public static double AverageOrDefault<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            return enumerable.Select(selector).DefaultIfEmpty().Average();
        }
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator enumerator) where T : class
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current as T;
        }
        public static void CustomForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}
