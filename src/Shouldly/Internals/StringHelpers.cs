using Shouldly.Internals;

namespace Shouldly;

static class StringHelpers
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
                if (value is IEnumerableProxy proxy)
                {
                    objectType = proxy.ProxiedValue.GetType();
                    value = proxy.ProxiedValue;
                }

                if (value.ToString() != objectType.FullName)
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

        if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)) {
            var key = objectType.GetProperty("Key")!.GetValue(value, null);
            var v = objectType.GetProperty("Value")!.GetValue(value, null);
            return $"[{key.ToStringAwesomely()} => {v.ToStringAwesomely()}]";
        }

        var toString = value.ToString();
        if (toString == objectType.FullName)
            return $"{value} ({value.GetHashCode():D6})";

        return toString; // ToString() may return null.
    }

    extension(string pascal)
    {
        internal string PascalToSpaced()
        {
            return Regex.Replace(pascal, "([A-Z])", match => " " + match.Value.ToLower()).Trim();
        }

        internal string Quotify() =>
            pascal.Replace('\'', '"');

        internal string StripWhitespace() =>
            Regex.Replace(pascal, @"[\r\n\t\s]", "");

        internal string CollapseWhitespace() =>
            Regex.Replace(pascal, @"[\r\n\t\s]+", " ");

        internal string StripLambdaExpressionSyntax() =>
            Regex.Replace(pascal, @"^\(*\s*\)*\s*=>\s*", "");

        internal string RemoveVariableAssignment()
        {
            var collapseWhitespace = Regex.Replace(pascal, @"^\w*\s+\w*\s*=[^>]\s*", "");
            collapseWhitespace = Regex.Replace(collapseWhitespace, @"\(\)\s*=\>\s*", "");
            return collapseWhitespace;
        }

        internal string RemoveBlock() =>
            Regex.Replace(pascal, @"^\s*({|\()\s*(?<inner>.*)\s*(}|\))$", "${inner}");

        internal string Clip(int maximumStringLength)
        {
            if (pascal.Length > maximumStringLength)
            {
                pascal = pascal[..maximumStringLength];
            }

            return pascal;
        }

        internal string Clip(int maximumStringLength, string ellipsis)
        {
            if (pascal.Length > maximumStringLength)
            {
                pascal = pascal[..maximumStringLength] + ellipsis;
            }

            return pascal;
        }
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
                    return $"\\u{(int)c:X};";
            }
        }

        return c.ToString();
    }

    internal static string? NormalizeLineEndings(this string? s) =>
        s == null ? null : Regex.Replace(s, @"\r\n?", "\n");

    extension<T>(IEnumerable<T> enumerable) where T : class?
    {
        private string CommaDelimited() =>
            enumerable.DelimitWith(", ");

        string DelimitWith(string? separator) =>
            string.Join(separator,
                enumerable.Select(i =>
                    {
                        if (Equals(i, null))
                            return null;
                        return i.ToString();
                    })
                    .ToArray());
    }

    private static string ToStringAwesomely(this Enum value) =>
        value.GetType().Name + "." + value;

    private static string ToStringAwesomely(this DateTime value) =>
        value.ToString("o");
}