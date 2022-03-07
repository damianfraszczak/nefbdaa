using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<string> GetModelErrors(this ModelStateDictionary errDictionary)
        {
            var errors = errDictionary.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            return errors;
        }

        public static Dictionary<string, string> GetModelErrorsWithKeys(this ModelStateDictionary errDictionary)
        {
            var errors = new Dictionary<string, string>();
            errDictionary.Where(k => k.Value.Errors.Count > 0).ToList().ForEach(i =>
            {
                var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                errors.Add(i.Key, er);
            });
            return errors;
        }
    }
}
