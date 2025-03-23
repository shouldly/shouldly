using System.ComponentModel;
using JetBrains.Annotations;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeEnumerableTestExtensions
{
    /// <summary>
    /// Asserts that the enumerable contains the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, string? customMessage = null)
    {
        if (!actual.Contains(expected))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable contains the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        if (!actual.Contains(expected, comparer))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable does not contain the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, string? customMessage = null)
    {
        if (actual.Contains(expected))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable does not contain the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        if (actual.Contains(expected, comparer))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the enumerable contains the expected number of elements matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null)
    {
        var condition = elementPredicate.Compile();
        var actualCount = actual.Count(condition);
        if (actualCount != expectedCount)
        {
            throw new ShouldAssertException(new ShouldContainWithCountShouldlyMessage(elementPredicate.Body, actual, expectedCount, customMessage).ToString());
        }
    }
    
    /// <summary>
    /// Asserts that the enumerable contains at least one element matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null)
    {
        var condition = elementPredicate.Compile();
        if (!actual.Any(condition))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable does not contain any elements matching the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null)
    {
        var condition = elementPredicate.Compile();
        if (actual.Any(condition))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(elementPredicate.Body, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that all elements in the enumerable satisfy the predicate.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldAllBe<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null)
    {
        var condition = elementPredicate.Compile();
        var actualResults = actual.Where(part => !condition(part));
        if (actualResults.Any())
            throw new ShouldAssertException(new ActualFilteredWithPredicateShouldlyMessage(elementPredicate.Body, actualResults, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the enumerable is empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeEmpty<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null)
    {
        if (actual == null || actual.Any())
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable is not empty.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeEmpty<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null)
    {
        if (actual == null || !actual.Any())
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable contains exactly one element and returns it.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldHaveSingleItem<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null)
    {
        if (actual == null || actual.Count() != 1)
            throw new ShouldAssertException(new ExpectedShouldlyMessage(actual, customMessage).ToString());
    
        return actual.Single();
    }

    /// <summary>
    /// Asserts that the enumerable contains a float value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance, string? customMessage = null)
    {
        if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
            throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable contains a double value within the specified tolerance.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance, string? customMessage = null)
    {
        if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
            throw new ShouldAssertException(new ExpectedActualToleranceShouldlyMessage(expected, actual, tolerance, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the enumerable is a subset of the expected enumerable.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string? customMessage = null)
    {
        if (actual.Equals(expected))
            return;
    
        var missing = actual.Except(expected);
        if (missing.Any())
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable is a subset of the expected enumerable using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        if (actual.Equals(expected))
            return;
    
        var missing = actual.Except(expected, comparer);
        if (missing.Any())
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the enumerable contains only unique elements.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this IEnumerable<T> actual, string? customMessage = null)
    {
        var duplicates = GetDuplicates(actual);
        if (duplicates.Any())
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(actual, duplicates, customMessage).ToString());
    }
    
    /// <summary>
    /// Asserts that the enumerable contains only unique elements using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this IEnumerable<T> actual, IEqualityComparer<T> comparer)
    {
        ShouldBeUnique(actual, comparer, null);
    }
    
    /// <summary>
    /// Asserts that the enumerable contains only unique elements using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeUnique<T>(this IEnumerable<T> actual, IEqualityComparer<T> comparer, string? customMessage)
    {
        var duplicates = GetDuplicates(actual, comparer);
        if (duplicates.Any())
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(actual, duplicates, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the string enumerable equals the expected enumerable with the specified case sensitivity.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, string? customMessage = null)
    {
        actual.AssertAwesomelyWithCaseSensitivity(
            v => Is.EnumerableStringEqualWithCaseSensitivity(v, expected, caseSensitivity),
            actual,
            expected,
            caseSensitivity,
            customMessage);
    }

    /// <summary>
    /// Asserts that the enumerable is in ascending order.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, string? customMessage = null)
    {
        ShouldBeInOrder(actual, SortDirection.Ascending, customMessage);
    }
    
    /// <summary>
    /// Asserts that the enumerable is in the specified order.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, string? customMessage = null)
    {
        ShouldBeInOrder(actual, expectedSortDirection, (IComparer<T>?)null, customMessage);
    }
    
    /// <summary>
    /// Asserts that the enumerable is in the specified order using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T>? customComparer, string? customMessage = null)
    {
        if (customComparer == null)
            customComparer = Comparer<T>.Default;
    
        var isOutOfOrder = expectedSortDirection == SortDirection.Ascending
            ? (Func<int, bool>)(r => r > 0) // If 'ascending', the previous value should never be greater than the current value
            : r => r < 0;  // If 'descending', the previous value should never be less than the current value
    
        ShouldBeInOrder(actual, expectedSortDirection, (x, y) => isOutOfOrder(customComparer.Compare(x, y)), customMessage);
    }

    private static HashSet<T> GetDuplicates<T>(IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
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

    private static void ShouldBeInOrder<T>(IEnumerable<T> actual, SortDirection expectedSortDirection, Func<T, T, bool> isOutOfOrder, string? customMessage)
    {
        var previousItem = default(T);
        var currentIndex = -1;

        foreach (var currentItem in actual)
        {
            if (++currentIndex > 0 // We only need to start comparing once we've passed the first item in the list
                && isOutOfOrder(previousItem!, currentItem))
            {
                throw new ShouldAssertException(
                    new ExpectedOrderShouldlyMessage(actual, expectedSortDirection, currentIndex, currentItem, customMessage).ToString());
            }

            previousItem = currentItem;
        }
    }

    /// <summary>
    /// Asserts that the elements in the enumerable are of the specified types.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOfTypes<T>(this IEnumerable<T> actual, params Type[] expected)
    {
        ShouldBeOfTypes(actual, expected, null);
    }
    
    /// <summary>
    /// Asserts that the elements in the enumerable are of the specified types.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOfTypes<T>(this IEnumerable<T> actual, Type[] expected, string? customMessage)
    {
        actual.Select(x => x!.GetType()).ToArray().ShouldBe(expected, customMessage);
    }
}