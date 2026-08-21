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

    public static bool Equal<T>(T? expected, T? actual) =>
        Equal(expected, actual, GetEqualityComparer<T>());

    public static bool Equal<T>(T? expected, T? actual, IEqualityComparer<T> comparer) =>
        comparer.Equals(actual, expected);

    private static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer? innerComparer = null) =>
        new Internals.EqualityComparer<T>(innerComparer);

    /// <summary>
    /// The comparer used to compare a single value of type <typeparamref name="T"/>: Shouldly's structural
    /// comparer, except for <see cref="string"/> and the types registered in
    /// <see cref="ShouldlyConfiguration.CompareAsObjectTypes"/>, which are compared with their own
    /// <see cref="object.Equals(object)"/> rather than being walked as enumerables. Shared so that every
    /// assertion comparing one value — <c>ShouldBe</c>, the dictionary value assertions — agrees on equality.
    /// </summary>
    public static IEqualityComparer<T> ScalarComparerFor<T>() =>
        ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!) || typeof(T) == typeof(string)
            ? new ObjectEqualityComparer<T>()
            : GetEqualityComparer<T>();

    public static bool Equal<T>(IEnumerable<T>? actual, IEnumerable<T>? expected) =>
        // The initial implementation of this functionality call Equal(actualEnum.Current, expectedEnum.Current), which
        // internally calls GetEqualityComparer<T>() to get the comparer.  As this is the case, we can just call GetEqualityComparer<T>()
        // and move the contents to the overload that supports accepting the comparer method.
        Equal(actual, expected, GetEqualityComparer<T>());

    public static bool Equal<T>(IEnumerable<T>? actual, IEnumerable<T>? expected, IEqualityComparer<T> comparer)
    {
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

    public static bool EqualIgnoreOrder<T>(IEnumerable<T>? actual, IEnumerable<T>? expected) =>
        // The initial implementation of this functionality call Equal(actualEnum.Current, expectedEnum.Current), which
        // internally calls GetEqualityComparer<T>() to get the comparer.  As this is the case, we can just call GetEqualityComparer<T>()
        // and move the contents to the overload that supports accepting the comparer method.
        EqualIgnoreOrder(actual, expected, GetEqualityComparer<T>());

    public static bool EqualIgnoreOrder<T>(IEnumerable<T>? actual, IEnumerable<T>? expected, IEqualityComparer<T> comparer)
    {
        if (actual == null && expected == null)
            return true;

        if (actual == null || expected == null)
            return false;

        if (actual is ICollection actualCollection &&
            expected is ICollection collection &&
            actualCollection.Count != collection.Count)
            return false;

        var expectedList = expected.ToList();
        foreach (var actualElement in actual)
        {
            // Match by index rather than FirstOrDefault + Remove: when no element is equivalent,
            // FirstOrDefault returns default(T), and Remove(default(T)) would succeed whenever
            // default(T) (e.g. 0 or null) is present in the list.
            var index = expectedList.FindIndex(x => Equal(x, actualElement, comparer));
            if (index < 0)
                return false;

            expectedList.RemoveAt(index);
        }

        return expectedList.Count == 0;
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