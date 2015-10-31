﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal static class StringHelpers
    {
        private static string CommaDelimited<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.DelimitWith(", ");
        }

        private static string DelimitWith<T>(this IEnumerable<T> enumerable, string separator) where T : class
        {
            return String.Join(separator, enumerable.Select(i => Equals(i, default(T)) ? null : i.ToString()).ToArray());
        }

        private static string ToStringAwesomely(this Enum value)
        {
            return value.GetType().Name +"."+ value;
        }

        internal static string ToStringAwesomely(this object value)
        {
            if (value == null)
                return "null";

            if (value is string)
                return "\"" + value + "\"";

            if (value is IEnumerable)
            {
                var objects = value.As<IEnumerable>().Cast<object>();
                var inspect = "[" + objects.Select(o => o.ToStringAwesomely()).CommaDelimited() + "]";
                if (inspect == "[]" && value.ToString() != value.GetType().FullName)
                {
                    inspect += " (" + value + ")";
                }
                return inspect;
            }

            if (value is Enum)
                return value.As<Enum>().ToStringAwesomely();

            if (value is ConstantExpression)
            {
                return value.As<ConstantExpression>().Value.ToStringAwesomely();
            }

            if (value is MemberExpression)
            {
                var member = value.As<MemberExpression>();
                var constant = member.Expression.As<ConstantExpression>();
                var info = member.Member.As<FieldInfo>();
                return info.GetValue(constant.Value).ToStringAwesomely();
            }

#if net40
            if (value is BinaryExpression)
            {
                return ExpressionToString.ExpressionStringBuilder.ToString(value.As<BinaryExpression>());
            }
#endif

            return value == null ? "null" : value.ToString();
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
            if (Char.IsControl(c) || char.IsWhiteSpace(c))
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
            return c.ToString(CultureInfo.InvariantCulture);
        }

        internal static string NormalizeLineEndings(this string s)
        {
            if (s == null) return null;
            return Regex.Replace(s, @"\r\n?", "\n");
        }
    }
}