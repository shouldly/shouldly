using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly
{
    public static class StringHelpers
    {
        public static string Inspect(this object value)
        {
            if (value is string)
                return "\"" + value + "\"";

            if (value is IEnumerable)
            {
                var objects = ((IEnumerable)value).Cast<object>();
                return "[" + objects.Select(o => o.Inspect()).Delimited() + "]";
            }

            return value == null ? "null" : value.ToString();
        }

        public static string Delimited<T>(this IEnumerable<T> enumerable, string separator) where T : class
        {
            return string.Join(separator, enumerable.Select(i => Equals(i, default(T)) ? null : i.ToString()).ToArray());
        }

        public static string Delimited<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.Delimited(", ");
        }

        public static string PascalToSpaced(this string pascal)
        {
            return Regex.Replace(pascal, @"([A-Z])", match => " " + match.Value.ToLower()).Trim();
        }
    }
}