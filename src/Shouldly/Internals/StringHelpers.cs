using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal static class StringHelpers
    {
        internal static string ToStringAwesomely(this object value)
        {
            if (value == null)
                return "null";

            if (value is string)
                return "\"" + value + "\"";

            var type = value.GetType();

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


            if (value is IEnumerable)
            {
                var objects = value.As<IEnumerable>().Cast<object>();
                var inspect = "[" + objects.Select(o => o.ToStringAwesomely()).CommaDelimited() + "]";
                if (inspect == "[]" && value.ToString() != type.FullName)
                {
                    inspect += " (" + value + ")";
                }
                return inspect;
            }

            if (value is Enum)
                return value.As<Enum>().ToStringAwesomely();

            if (value is DateTime)
                return value.As<DateTime>().ToStringAwesomely();

            if (value is ConstantExpression)
                return value.As<ConstantExpression>().Value.ToStringAwesomely();

            if (value is MemberExpression)
            {
                var member = value.As<MemberExpression>();
                var constant = member.Expression.As<ConstantExpression>();
                var info = member.Member.As<FieldInfo>();
                return info.GetValue(constant.Value).ToStringAwesomely();
            }

            if (value is BinaryExpression)
            {
                return ExpressionToString.ExpressionStringBuilder.ToString(value.As<BinaryExpression>());
            }

#if NewReflection
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var key = type.GetRuntimeProperty("Key").GetValue(value, null);
                var v = type.GetRuntimeProperty("Value").GetValue(value, null);
                return $"[{key.ToStringAwesomely()} => {v.ToStringAwesomely()}]";
            }
#else
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)){
                var key = type.GetProperty("Key").GetValue(value, null);
                var v = type.GetProperty("Value").GetValue(value, null);
                return $"[{key.ToStringAwesomely()} => {v.ToStringAwesomely()}]";
            }
#endif


            var toString = value.ToString();
            if (toString == type.FullName)
                return $"{value} ({value.GetHashCode()})";

            return toString;
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
                        return string.Format("\\u{0:X};", (int)c);
                }
            }
            return c.ToString();
        }

        internal static bool IsNullOrWhiteSpace(this string s)
        {
#if NET35
            return string.IsNullOrEmpty(s.Trim());
#else
            return string.IsNullOrWhiteSpace(s);
#endif
        }

        internal static string NormalizeLineEndings(this string s)
        {
            return s == null ? null : Regex.Replace(s, @"\r\n?", "\n");
        }

        static string CommaDelimited<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.DelimitWith(", ");
        }

        static string DelimitWith<T>(this IEnumerable<T> enumerable, string separator) where T : class
        {
            return string.Join(separator, enumerable.Select(i => Equals(i, default(T)) ? null : i.ToString()).ToArray());
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