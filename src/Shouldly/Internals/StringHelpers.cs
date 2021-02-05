using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Shouldly.Internals;

namespace Shouldly
{
    internal static class StringHelpers
    {
        internal static string? ToStringAwesomely(this object? value)
        {
            if (value == null)
                return "null";

            if (value is string)
                return "\"" + value + "\"";

            if (value is decimal)
                return value + "m";

            if (value is double)
                return value + "d";

            if (value is float)
                return value + "f";

            if (value is long)
                return value + "L";

            if (value is uint)
                return value + "u";

            if (value is ulong)
                return value + "uL";

            var objectType = value.GetType();

            if (value.TryGetEnumerable(out var enumerable))
            {
                var objects = enumerable.Cast<object>();
                var inspect = "[" + objects.Select(o => o.ToStringAwesomely()).CommaDelimited() + "]";
                if (inspect == "[]")
                {
                    if (value is IProxy proxy)
                    {
                        objectType = proxy.ProxiedValue.GetType();
                        value = proxy.ProxiedValue;
                    }
                    if(value.ToString() != objectType.FullName)
                    {
                        inspect += " (" + value + ")";
                    }
                }

                return inspect;
            }

            if (value is Enum @enum)
                return @enum.ToStringAwesomely();

            if (value is DateTime dateTime)
                return dateTime.ToStringAwesomely();

            if (value is ConstantExpression constantExpression)
                return constantExpression.Value.ToStringAwesomely();

            if (value is MemberExpression { Expression: ConstantExpression constant, Member: FieldInfo info })
            {
                return info.GetValue(constant.Value).ToStringAwesomely();
            }

            if (value is BinaryExpression binaryExpression)
            {
                return ExpressionToString.ExpressionStringBuilder.ToString(binaryExpression);
            }

            if (objectType.IsGenericType() && objectType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)){
                var key = objectType.GetProperty("Key")!.GetValue(value, null);
                var v = objectType.GetProperty("Value")!.GetValue(value, null);
                return $"[{key.ToStringAwesomely()} => {v.ToStringAwesomely()}]";
            }

            var toString = value.ToString();
            if (toString == objectType.FullName)
                return $"{value} ({value.GetHashCode()})";

            return toString; // ToString() may return null.
        }

        internal static string PascalToSpaced(this string pascal)
        {
            return Regex.Replace(pascal, @"([A-Z])", match => " " + match.Value.ToLower()).Trim();
        }

        internal static string Quotify(this string input)
        {
            return input.Replace('\'', '"');
        }

        internal static string StripWhitespace(this string input)
        {
            return Regex.Replace(input, @"[\r\n\t\s]", "");
        }

        internal static string CollapseWhitespace(this string input)
        {
            var collapseWhitespace = Regex.Replace(input, @"[\r\n\t\s]+", " ");
            return collapseWhitespace;
        }

        internal static string StripLambdaExpressionSyntax(this string input)
        {
            var result = Regex.Replace(input, @"^\(*\s*\)*\s*=>\s*", "");
            return result;
        }

        internal static string RemoveVariableAssignment(this string input)
        {
            var collapseWhitespace = Regex.Replace(input, @"^\w*\s+\w*\s*=[^>]\s*", "");
            collapseWhitespace = Regex.Replace(collapseWhitespace, @"\(\)\s*=\>\s*", "");
            return collapseWhitespace;
        }

        internal static string RemoveBlock(this string input)
        {
            var result = Regex.Replace(input, @"^\s*({|\()\s*(?<inner>.*)\s*(}|\))$", "${inner}");
            return result;
        }

        internal static string Clip(this string stringToClip, int maximumStringLength)
        {
            if (stringToClip.Length > maximumStringLength)
            {
                stringToClip = stringToClip.Substring(0, maximumStringLength);
            }
            return stringToClip;
        }

        internal static string Clip(this string stringToClip, int maximumStringLength, string ellipsis)
        {
            if (stringToClip.Length > maximumStringLength)
            {
                stringToClip = stringToClip.Substring(0, maximumStringLength) + ellipsis;
            }
            return stringToClip;
        }

        internal static string ToSafeString(this char c)
        {
            if (char.IsControl(c) || char.IsWhiteSpace(c))
            {
                switch (c)
                {
                    case '\r':
                        return @"\r";
                    case '\n':
                        return @"\n";
                    case '\t':
                        return @"\t";
                    case '\a':
                        return @"\a";
                    case '\v':
                        return @"\v";
                    case '\f':
                        return @"\f";
                    case ' ':
                        return @"\s";
                    default:
                        return $"\\u{(int) c:X};";
                }
            }
            return c.ToString();
        }

        internal static bool IsNullOrWhiteSpace(this string? s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        internal static string? NormalizeLineEndings(this string? s)
        {
            return s == null ? null : Regex.Replace(s, @"\r\n?", "\n");
        }

        static string CommaDelimited<T>(this IEnumerable<T> enumerable) where T : class?
        {
            return enumerable.DelimitWith(", ");
        }

        static string DelimitWith<T>(this IEnumerable<T> enumerable, string? separator) where T : class?
        {
            return string.Join(separator, enumerable.Select(i => Equals(i, null) ? null : i.ToString()).ToArray());
        }

        static string ToStringAwesomely(this Enum value)
        {
            return value.GetType().Name + "." + value;
        }

        static string ToStringAwesomely(this DateTime value)
        {
            return value.ToString("o");
        }
    }
}