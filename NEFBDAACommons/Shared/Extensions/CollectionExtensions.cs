using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class CollectionExtensions
    {

        public static dynamic ToDynamicObject<TValue>(this Dictionary<string, TValue> dict)
        {
            var obj = dict.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                            (a, p) => { a.Add(p.Key, p.Value); return a; });
            return obj;
        }

   
    }
}
