using NEFBDAACommons.DynamicForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Database.Models
{
    public abstract class Enumeration : IComparable, ISelectFieldOptionViewModel
    {
        public string Name { get; private set; }

        public int Id { get; private set; }
        public bool HideFromFilter { get; set; } = false;

        public bool HideFromEdit { get; set; } = false;

        public Dictionary<string, string> OptionAdditionalInfo => throw new NotImplementedException();

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public string OptionId => Id + "";

        public string OptionText => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return GetAll<T>(typeof(T));
        }

        public static IEnumerable<T> GetAll<T>(Type type) where T : Enumeration
        {
            var fields = type.GetFields(BindingFlags.Public |
                                        BindingFlags.Static |
                                        BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration) other).Id);

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = Parse<T, string>(displayName, "name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}