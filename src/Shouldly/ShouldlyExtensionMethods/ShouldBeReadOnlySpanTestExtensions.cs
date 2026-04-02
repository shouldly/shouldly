using System.ComponentModel;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

/// <summary>
/// Extension methods for read-only span assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeReadOnlySpanTestExtensions
{
    /// <summary>
    /// Asserts that the read-only span contains the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this ReadOnlySpan<T> actual, T expected, string? customMessage = null) where T : IEquatable<T>?
    {
        foreach (var item in actual)
        {
            if (item!.Equals(expected))
                return;
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span contains the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this ReadOnlySpan<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null) where T : IEquatable<T>?
    {
        foreach (var item in actual)
        {
            if (comparer.Equals(item, expected))
                return;
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span does not contain the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this ReadOnlySpan<T> actual, T expected, string? customMessage = null) where T : IEquatable<T>?
    {
        foreach (var item in actual)
        {
            if (item!.Equals(expected))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual.ToArray(), customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that the read-only span does not contain the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this ReadOnlySpan<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null) where T : IEquatable<T>?
    {
        for (var i = 0; i < actual.Length; i++)
        {
            if (comparer.Equals(actual[i], expected))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual.ToArray(), customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that the read-only span contains the expected number of elements matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this ReadOnlySpan<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null) where T : IEquatable<T>?
    {
        var condition = elementPredicate.Compile();

        var actualCount = 0;
        for (var i = 0; i < actual.Length; i++)
        {
            if (condition(actual[i]))
            {
                if (++actualCount > expectedCount)
                    break;
            }
        }

        if (actualCount != expectedCount)
        {
            throw new ShouldAssertException(new ShouldContainWithCountShouldlyMessage(elementPredicate.Body, actual.ToArray(), expectedCount, customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that the read-only span contains at least one element matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this ReadOnlySpan<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null) where T : IEquatable<T>?
    {
        var condition = elementPredicate.Compile();

        for (var i = 0; i < actual.Length; i++)
        {
            if (condition(actual[i]))
                return;
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span does not contain any elements matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this ReadOnlySpan<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null) where T : IEquatable<T>?
    {
        var condition = elementPredicate.Compile();

        for (var i = 0; i < actual.Length; i++)
        {
            if (condition(actual[i]))
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual.ToArray(), customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that all elements in the read-only span satisfy the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldAllBe<T>(this ReadOnlySpan<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null) where T : IEquatable<T>?
    {
        var condition = elementPredicate.Compile();

        var actualResults = new List<T>();
        for (var i = 0; i < actual.Length; i++)
        {
            if (!condition(actual[i]))
                actualResults.Add(actual[i]);
        }

        if (actualResults.Count != 0)
            throw new ShouldAssertException(new ActualFilteredWithPredicateShouldlyMessage(elementPredicate.Body, actualResults, actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeEmpty<T>(this ReadOnlySpan<T> actual, string? customMessage = null) where T : IEquatable<T>?
    {
        if (!actual.IsEmpty)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span is not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeEmpty<T>(this ReadOnlySpan<T> actual, string? customMessage = null) where T : IEquatable<T>?
    {
        if (actual.IsEmpty)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span contains exactly one element and returns it.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldHaveSingleItem<T>(this ReadOnlySpan<T> actual, string? customMessage = null) where T : IEquatable<T>?
    {
        if (actual.Length != 1)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual.ToArray(), customMessage).ToString());

        return actual[0];
    }

    /// <summary>
    /// Asserts that the read-only span contains a float value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain(this ReadOnlySpan<float> actual, float expected, double tolerance, string? customMessage = null)
    {
        for (var i = 0; i < actual.Length; i++)
        {
            if (Math.Abs(expected - actual[i]) < tolerance)
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual.ToArray(), tolerance, customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that the read-only span contains a double value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain(this ReadOnlySpan<double> actual, double expected, double tolerance, string? customMessage = null)
    {
        for (var i = 0; i < actual.Length; i++)
        {
            if (Math.Abs(expected - actual[i]) < tolerance)
                throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual.ToArray(), tolerance, customMessage).ToString());
        }
    }

    /// <summary>
    /// Asserts that the read-only span is a subset of the expected read-only span.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSubsetOf<T>(this ReadOnlySpan<T> actual, ReadOnlySpan<T> expected, string? customMessage = null)
    {
        if (actual.Length <= expected.Length)
        {
            var missing = new HashSet<T>();
            foreach (var item in actual)
            {
                missing.Add(item);
            }
            foreach (var item in expected)
            {
                missing.Remove(item);
            }

            if (missing.Count == 0)
                return;
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected.ToArray(), actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span is a subset of the expected read-only span using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSubsetOf<T>(this ReadOnlySpan<T> actual, ReadOnlySpan<T> expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        if (actual.Length <= expected.Length)
        {
            var missing = new HashSet<T>(comparer);
            foreach (var item in actual)
            {
                missing.Add(item);
            }
            foreach (var item in expected)
            {
                missing.Remove(item);
            }

            if (missing.Count == 0)
                return;
        }

        throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected.ToArray(), actual.ToArray(), customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span contains only unique elements.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this ReadOnlySpan<T> actual, string? customMessage = null)
    {
        var duplicates = GetDuplicates(actual);
        if (duplicates.Count != 0)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(actual.ToArray(), duplicates, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the read-only span contains only unique elements using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this ReadOnlySpan<T> actual, IEqualityComparer<T> comparer)
    {
        ShouldBeUnique(actual, comparer, null);
    }

    /// <summary>
    /// Asserts that the read-only span contains only unique elements using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this ReadOnlySpan<T> actual, IEqualityComparer<T> comparer, string? customMessage)
    {
        var duplicates = GetDuplicates(actual, comparer);
        if (duplicates.Count != 0)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(actual.ToArray(), duplicates, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the string read-only span equals the expected read-only span with the specified case sensitivity.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this ReadOnlySpan<string> actual, ReadOnlySpan<string> expected, Case caseSensitivity, string? customMessage = null)
    {
        var actualArray = actual.ToArray();
        var expectedArray = expected.ToArray();
        actualArray.AssertAwesomelyWithCaseSensitivity(
            v => Is.EnumerableStringEqualWithCaseSensitivity(v, expectedArray, caseSensitivity),
            actualArray,
            expectedArray,
            caseSensitivity,
            customMessage);
    }

    /// <summary>
    /// Asserts that the read-only span is in ascending order.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this ReadOnlySpan<T> actual, string? customMessage = null)
    {
        ShouldBeInOrder(actual, SortDirection.Ascending, customMessage);
    }

    /// <summary>
    /// Asserts that the read-only span is in the specified order.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this ReadOnlySpan<T> actual, SortDirection expectedSortDirection, string? customMessage = null)
    {
        ShouldBeInOrder(actual, expectedSortDirection, (IComparer<T>?)null, customMessage);
    }

    /// <summary>
    /// Asserts that the read-only span is in the specified order using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this ReadOnlySpan<T> actual, SortDirection expectedSortDirection, IComparer<T>? customComparer, string? customMessage = null)
    {
        if (customComparer == null)
            customComparer = Comparer<T>.Default;

        var isOutOfOrder = expectedSortDirection == SortDirection.Ascending
            ? (Func<int, bool>)(r => r > 0) // If 'ascending', the previous value should never be greater than the current value
            : r => r < 0;  // If 'descending', the previous value should never be less than the current value

        ShouldBeInOrder(actual, expectedSortDirection, (x, y) => isOutOfOrder(customComparer.Compare(x, y)), customMessage);
    }

    private static HashSet<T> GetDuplicates<T>(this ReadOnlySpan<T> items, IEqualityComparer<T>? comparer = null)
    {
        var uniqueItems = new HashSet<T>(comparer);
        var duplicates = new HashSet<T>(comparer);

        foreach (var item in items)
        {
            if (!uniqueItems.Add(item))
            {
                duplicates.Add(item);
            }
        }

        return duplicates;
    }

    private static void ShouldBeInOrder<T>(ReadOnlySpan<T> actual, SortDirection expectedSortDirection, Func<T, T, bool> isOutOfOrder, string? customMessage)
    {
        var previousItem = default(T);
        var currentIndex = -1;

        foreach (var currentItem in actual)
        {
            if (++currentIndex > 0 // We only need to start comparing once we've passed the first item in the list
                && isOutOfOrder(previousItem!, currentItem))
            {
                throw new ShouldAssertException(
                    new ExpectedOrderShouldlyMessage(actual.ToArray(), expectedSortDirection, currentIndex, currentItem, customMessage).ToString());
            }

            previousItem = currentItem;
        }
    }

    /// <summary>
    /// Asserts that the elements in the read-only span are of the specified types.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOfTypes<T>(this ReadOnlySpan<T> actual, params Type[] expected)
    {
        ShouldBeOfTypes(actual, expected, null);
    }

    /// <summary>
    /// Asserts that the elements in the read-only span are of the specified types.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOfTypes<T>(this ReadOnlySpan<T> actual, Type[] expected, string? customMessage)
    {
        var actualTypes = new HashSet<Type>();

        foreach (var item in actual)
        {
            actualTypes.Add(item!.GetType());
        }

        actualTypes.ToArray().ShouldBe(expected, customMessage);
    }
}