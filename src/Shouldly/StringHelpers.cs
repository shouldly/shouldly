using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Shouldly
{
    public static class StringHelpers
    {
        public static string CommaDelimited<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.DelimitWith(", ");
        }

        public static string DelimitWith<T>(this IEnumerable<T> enumerable, string separator) where T : class
        {
            return string.Join(separator, enumerable.Select(i => Equals(i, default(T)) ? null : i.ToString()).ToArray());
        }

        public static string Inspect(this Enum value)
        {
            return value.GetType().Name +"."+ value;
        }

        public static string Inspect(this object value)
        {
            if (value is string)
                return "\"" + value + "\"";

            if (value is IEnumerable)
            {
                var objects = ((IEnumerable)value).Cast<object>();
                return "[" + objects.Select(o => o.Inspect()).CommaDelimited() + "]";
            }

            if (value is Enum)
                return value.As<Enum>().Inspect();

            if (value is ConstantExpression)
            {
                return value.As<ConstantExpression>().Value.Inspect();
            }

            return value == null ? "null" : value.ToString();
        }

        public static string PascalToSpaced(this string pascal)
        {
            return Regex.Replace(pascal, @"([A-Z])", match => " " + match.Value.ToLower()).Trim();
        }

        public static string Quotify(this string input)
        {
            return input.Replace('\'', '"');
        }

        public static string StripWhitespace(this string input)
        {
            return Regex.Replace(input, @"[\r\n\t\s]", "");
        }
    }
}