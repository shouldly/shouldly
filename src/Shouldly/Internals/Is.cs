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

    public static bool Equal<T>(T? actual, T? expected) =>
        Equal(actual, expected, GetEqualityComparer<T>());

    /// <summary>
    /// Whether <paramref name="actual"/> equals <paramref name="expected"/>. By design, the comparer is called as
    /// <c>comparer.Equals(expected, actual)</c>, so the default comparer calls <c>Equals</c> on the expected
    /// value: a broken <c>Equals</c> on the value under test can't make an assertion pass. Every comparison
    /// between an actual and an expected value goes through here so that the order is the same everywhere
    /// (issue #1076).
    /// </summary>
    public static bool Equal<T>(T? actual, T? expected, IEqualityComparer<T> comparer) =>
        comparer.Equals(expected, actual);

    internal static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer? innerComparer = null) =>
        new Internals.EqualityComparer<T>(innerComparer);

    /// <summary>
    /// The comparer used to compare a single value of type <typeparamref name="T"/>: <see cref="Internals.Equality.ComparerFor{T}"/>.
    /// </summary>
    public static IEqualityComparer<T> ScalarComparerFor<T>() => Internals.Equality.ComparerFor<T>();

    public static bool Equal<T>(IEnumerable<T>? actual, IEnumerable<T>? expected) =>
        // The initial implementation of this functionality call Equal(actualEnum.Current, expectedEnum.Current), which
        // internally calls GetEqualityComparer<T>() to get the comparer.  As this is the case, we can just call GetEqualityComparer<T>()
        // and move the contents to the overload that supports accepting the comparer method.
        Equal(actual, expected, GetEqualityComparer<T>());

    public static bool Equal<T>(IEnumerable<T>? actual, IEnumerable<T>? expected, IEqualityComparer<T> comparer)
    {
        actual = Internals.NullEquivalence.Normalize(actual);
        expected = Internals.NullEquivalence.Normalize(expected);

        if (actual == null && expected == null)
            return true;
        if (actual == null || expected == null)
            return false;

        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        while (true)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData && !actualHasData)
                return true;

            if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, comparer))
            {
                return false;
            }
        }
    }

    public static bool Equal(IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        for (; ;)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData && !actualHasData)
                return true;

            if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
            {
                return false;
            }
        }
    }

    public static bool Equal(IEnumerable<float> actual, IEnumerable<float> expected, double tolerance)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        for (; ;)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData && !actualHasData)
                return true;

            if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
            {
                return false;
            }
        }
    }

    public static bool Equal(IEnumerable<double> actual, IEnumerable<double> expected, double tolerance)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        for (; ;)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData && !actualHasData)
                return true;

            if (expectedHasData != actualHasData || !Equal(actualEnum.Current, expectedEnum.Current, tolerance))
            {
                return false;
            }
        }
    }

    public static bool Equal(decimal actual, decimal expected, decimal tolerance) =>
        Math.Abs(actual - expected) <= tolerance;

    public static bool Equal(double actual, double expected, double tolerance) =>
        Math.Abs(actual - expected) <= tolerance;

    public static bool Equal(DateTime actual, DateTime expected, TimeSpan tolerance) =>
        (actual - expected).Duration() <= tolerance;

    public static bool Equal(DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance) =>
        (actual - expected).Duration() <= tolerance;

    public static bool Equal(TimeSpan actual, TimeSpan expected, TimeSpan tolerance) => (actual - expected).Duration() <= tolerance;

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

    public static bool StringEqualWithCaseSensitivity(string? actual, string? expected, Case caseSensitivity)
    {
        if (caseSensitivity == Case.Insensitive)
        {
            return StringComparer.OrdinalIgnoreCase.Equals(actual, expected);
        }

        return StringComparer.Ordinal.Equals(actual, expected);
    }

    public static bool EnumerableStringEqualWithCaseSensitivity(IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        for (; ;)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            var bothListsProcessed = !expectedHasData && !actualHasData;
            if (bothListsProcessed)
                return true;

            var listsHaveDifferentLengths = !expectedHasData || !actualHasData;
            if (listsHaveDifferentLengths)
                return false;

            if (!StringEqualWithCaseSensitivity(actualEnum.Current, expectedEnum.Current, caseSensitivity))
            {
                return false;
            }
        }
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