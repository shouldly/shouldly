using JetBrains.Annotations;

namespace Shouldly;

static class Is
{
    public static bool InRange<T>([DisallowNull] T comparable, T? from, T? to)
        where T : IComparable<T> =>
        comparable.CompareTo(from) >= 0 &&
        comparable.CompareTo(to) <= 0;

    public static bool Same(object? actual, object? expected)
    {
        if (actual == null && expected == null)
            return true;
        if (actual == null || expected == null)
            return false;

        return ReferenceEquals(actual, expected);
    }

    public static bool Equal(DateTime actual, DateTime expected, TimeSpan tolerance) =>
        (actual - expected).Duration() <= tolerance;

    public static bool Equal(DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance) =>
        (actual - expected).Duration() <= tolerance;

    public static bool Equal(TimeSpan actual, TimeSpan expected, TimeSpan tolerance)
    {
        try
        {
            return (actual - expected).Duration() <= tolerance;
        }
        catch (OverflowException)
        {
            // The difference is longer than any TimeSpan, so longer than any tolerance. Duration() overflows too, on a
            // difference of exactly TimeSpan.MinValue, which is a tick longer than TimeSpan.MaxValue.
            return false;
        }
    }

    public static bool StringMatchingRegex(string actual, [RegexPattern] string regexPattern) =>
        Regex.IsMatch(actual, regexPattern);

    public static bool StringContainingIgnoreCase(string? actual, string expected)
    {
        if (actual == null)
            return false;

        return actual.IndexOf(expected, StringComparison.OrdinalIgnoreCase) != -1;
    }

    public static bool StringContainingUsingCaseSensitivity(string? actual, string expected)
    {
        if (actual == null)
            return false;

        return actual.IndexOf(expected, StringComparison.Ordinal) != -1;
    }

    public static bool EndsWithUsingCaseSensitivity(string? actual, string expected, Case caseSensitivity)
    {
        if (actual == null)
            return false;

        if (caseSensitivity == Case.Insensitive)
        {
            return actual.EndsWith(expected, StringComparison.OrdinalIgnoreCase);
        }

        return actual.EndsWith(expected, StringComparison.Ordinal);
    }

    public static bool StringStartingWithUsingCaseSensitivity(string? actual, string expected, Case caseSensitivity)
    {
        if (actual == null)
            return false;

        if (caseSensitivity == Case.Insensitive)
        {
            return actual.StartsWith(expected, StringComparison.OrdinalIgnoreCase);
        }

        return actual.StartsWith(expected, StringComparison.Ordinal);
    }

    public static bool GreaterThanOrEqualTo<T>(T? comparable, T? expected)
        where T : IComparable<T>? =>
        Compare(comparable, expected) >= 0;

    public static bool GreaterThanOrEqualTo<T>(T? actual, T? expected, IComparer<T> comparer) =>
        Compare(actual, expected, comparer) >= 0;

    public static bool LessThanOrEqualTo<T>(T? comparable, T? expected)
        where T : IComparable<T>? =>
        Compare(comparable, expected) <= 0;

    public static bool LessThanOrEqualTo<T>(T? actual, T? expected, IComparer<T> comparer) => Compare(actual, expected, comparer) <= 0;

    public static bool GreaterThan<T>(T? comparable, T? expected)
        where T : IComparable<T>? =>
        Compare(comparable, expected) > 0;

    public static bool GreaterThan<T>(T? actual, T? expected, IComparer<T> comparer) =>
        Compare(actual, expected, comparer) > 0;

    public static bool LessThan<T>(T? comparable, T? expected)
        where T : IComparable<T>? =>
        Compare(comparable, expected) < 0;

    public static bool LessThan<T>(T? actual, T? expected, IComparer<T> comparer) =>
        Compare(actual, expected, comparer) < 0;

    private static decimal Compare<T>(T? actual, T? expected, IComparer<T> comparer) =>
        comparer.Compare(actual, expected);

    private static decimal Compare<T>(T? comparable, T? expected)
        where T : IComparable<T>?
    {
        if (!typeof(T).IsValueType)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (comparable == null)
                return expected == null ? 0 : -1;
            if (expected == null)
                return +1;
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        return comparable!.CompareTo(expected);
    }
}