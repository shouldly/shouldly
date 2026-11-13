using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;
using Shouldly.Internals.Failures;
using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

/// <summary>
/// Extension methods for enumerable assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeEnumerableTestExtensions
{
    /// <summary>
    /// Asserts that the enumerable contains the expected value, comparing elements as <c>ShouldBe</c> does.
    /// A set, or a dictionary's key collection, also contains what its own comparer finds.
    /// </summary>
    public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (!Membership.Contains(actual, expected))
            throw Fail.ShouldContain.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains the expected value using the specified comparer.
    /// </summary>
    public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (!Membership.Contains(actual, expected, comparer))
            throw Fail.ShouldContain.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable does not contain the expected value, comparing elements as <c>ShouldBe</c> does.
    /// A set, or a dictionary's key collection, also contains what its own comparer finds.
    /// </summary>
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (Membership.Contains(actual, expected))
            throw Fail.ShouldNotContain.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable does not contain the expected value using the specified comparer.
    /// </summary>
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (Membership.Contains(actual, expected, comparer))
            throw Fail.ShouldNotContain.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains the expected number of elements matching the predicate.
    /// </summary>
    public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var condition = elementPredicate.Compile();
        var actualCount = actual.Count(condition);
        if (actualCount != expectedCount)
        {
            throw Fail.ShouldContain.WithCondition(elementPredicate.Body, expectedCount).For(actual, customMessage, actualExpression);
        }
    }

    /// <summary>
    /// Asserts that the enumerable contains at least one element matching the predicate.
    /// </summary>
    public static void ShouldContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var condition = elementPredicate.Compile();
        if (!actual.Any(condition))
            throw Fail.ShouldContain.WithCondition(elementPredicate.Body).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable does not contain any elements matching the predicate.
    /// </summary>
    public static void ShouldNotContain<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var condition = elementPredicate.Compile();
        var matchingElements = actual.Where(condition).ToList();
        if (matchingElements.Count > 0)
            throw Fail.ShouldNotContain.WithConditionMatched(elementPredicate.Body, matchingElements).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that all elements in the enumerable satisfy the predicate.
    /// </summary>
    public static void ShouldAllBe<T>(this IEnumerable<T> actual, [InstantHandle] Expression<Func<T, bool>> elementPredicate, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var condition = elementPredicate.Compile();
        var actualResults = actual.Where(part => !condition(part));
        if (actualResults.Any())
            throw Fail.ShouldAllBe.With(elementPredicate.Body, actualResults).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is empty.
    /// </summary>
    public static void ShouldBeEmpty<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items;
        if (actual == null || actual.Any())
            throw Fail.ShouldBeEmpty.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is not empty.
    /// </summary>
    public static void ShouldNotBeEmpty<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items;
        if (actual == null || !actual.Any())
            throw Fail.ShouldNotBeEmpty.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is null or empty.
    /// </summary>
    public static void ShouldBeNullOrEmpty<T>(this IEnumerable<T>? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items;
        if (actual != null && actual.Any())
            throw Fail.ShouldBeNullOrEmpty.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is neither null nor empty. Equivalent to <see cref="ShouldNotBeEmpty{T}"/>, which also fails for null.
    /// </summary>
    public static void ShouldNotBeNullOrEmpty<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items;
        if (actual == null || !actual.Any())
            throw Fail.ShouldNotBeNullOrEmpty.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains exactly one element and returns it.
    /// </summary>
    public static T ShouldHaveSingleItem<T>([NotNull] this IEnumerable<T>? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items;
        if (actual == null || actual.Count() != 1)
            throw Fail.ShouldHaveSingleItem.For(actual, customMessage, actualExpression);

        return actual.Single();
    }

    /// <summary>
    /// Asserts that the enumerable contains the expected number of elements.
    /// </summary>
    public static void ShouldHaveCount<T>([NotNull] this IEnumerable<T>? actual, int expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = NullEquivalence.Normalize(actual);
        if (actual == null)
            throw Fail.ShouldHaveCount.With(expected).For(actual, customMessage, actualExpression);

        // A source that knows its count, such as a list or a query over an array, isn't read to count it: it passes
        // without being read, and fails reading only as far as the message shows.
        var counted = actual.TryGetNonEnumeratedCount(out var count);
        if (counted && count == expected)
            return;

        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (!counted)
            count = actual.Count();

        if (count != expected)
            throw Fail.ShouldHaveCount.With(expected, count).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains a float value within the specified tolerance.
    /// </summary>
    public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (!actual.Any(a => Is.Equal(a, expected, tolerance)))
            throw Fail.ShouldContain.Within(expected, tolerance).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains a double value within the specified tolerance.
    /// </summary>
    public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        if (!actual.Any(a => Is.Equal(a, expected, tolerance)))
            throw Fail.ShouldContain.Within(expected, tolerance).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is a subset of the expected enumerable, comparing elements as <c>ShouldBe</c> does.
    /// </summary>
    public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual.Equals(expected))
            return;

        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;

        var outside = Membership.Unmatched(actual, expected);
        if (outside.Count > 0)
            throw Fail.ShouldBeSubsetOf.With(expected, outside).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is a subset of the expected enumerable using the specified comparer.
    /// </summary>
    public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (actual.Equals(expected))
            return;

        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;

        var outside = actual.Except(expected, comparer).ToList();
        if (outside.Count > 0)
            throw Fail.ShouldBeSubsetOf.With(expected, outside).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains only unique elements, comparing elements as <c>ShouldBe</c> does.
    /// </summary>
    public static void ShouldBeUnique<T>(this IEnumerable<T> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var duplicates = Membership.Duplicates(actual);
        if (duplicates.Count > 0)
            throw Fail.ShouldBeUnique.With(duplicates).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable contains only unique elements using the specified comparer.
    /// </summary>
    public static void ShouldBeUnique<T>(this IEnumerable<T> actual, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var duplicates = Membership.Duplicates(actual, comparer);
        if (duplicates.Count > 0)
            throw Fail.ShouldBeUnique.With(duplicates).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the string enumerable equals the expected enumerable with the specified case sensitivity.
    /// </summary>
    public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;
        var equality = Equality.OfSequences(Equality.ForCase(caseSensitivity));
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the string enumerable equals the expected enumerable with the specified case sensitivity,
    /// optionally ignoring order.
    /// </summary>
    // This is a separate overload, rather than an optional ignoreOrder on the one above, so that overload
    // keeps its signature (binary compatibility, and positional customMessage callers). ignoreOrder is
    // required: if it were optional, ShouldBe(expected, caseSensitivity) would match both overloads with
    // defaults filled in and fail with CS0121 on every C# version, not something
    // [OverloadResolutionPriority] could settle on LangVersion < 13 (see issue #1278).
    public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, bool ignoreOrder,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!ignoreOrder)
        {
            actual.ShouldBe(expected, caseSensitivity, customMessage, actualExpression);
            return;
        }

        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;

        var equality = Equality.OfSequences(Equality.ForCase(caseSensitivity), ignoreOrder: true);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is in ascending order.
    /// </summary>
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldBeInOrder(actual, SortDirection.Ascending, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is in the specified order.
    /// </summary>
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldBeInOrder(actual, expectedSortDirection, (IComparer<T>?)null, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the enumerable is in the specified order using the specified comparer.
    /// </summary>
    public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T>? customComparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (customComparer == null)
            customComparer = Comparer<T>.Default;

        var isOutOfOrder = expectedSortDirection == SortDirection.Ascending
            ? (Func<int, bool>)(r => r > 0) // If 'ascending', the previous value should never be greater than the current value
            : r => r < 0;  // If 'descending', the previous value should never be less than the current value

        ShouldBeInOrder(actual, expectedSortDirection, (x, y) => isOutOfOrder(customComparer.Compare(x, y)), customMessage, actualExpression);
    }

#if !NET6_0_OR_GREATER
    // Polyfill of Enumerable.TryGetNonEnumeratedCount (introduced in .NET 6) for the netstandard2.0 target.
    private static bool TryGetNonEnumeratedCount<T>(this IEnumerable<T> source, out int count)
    {
        if (source is ICollection<T> genericCollection)
        {
            count = genericCollection.Count;
            return true;
        }

        if (source is ICollection collection)
        {
            count = collection.Count;
            return true;
        }

        count = 0;
        return false;
    }
#endif

    private static void ShouldBeInOrder<T>(IEnumerable<T> actual, SortDirection expectedSortDirection, Func<T, T, bool> isOutOfOrder, string? customMessage, string? actualExpression = null)
    {
        using var captured = CapturedSequence.Of(actual);
        actual = captured.Items!;
        var previousItem = default(T);
        var currentIndex = -1;

        foreach (var currentItem in actual)
        {
            if (++currentIndex > 0 // We only need to start comparing once we've passed the first item in the list
                && isOutOfOrder(previousItem!, currentItem))
            {
                throw Fail.ShouldBeInOrder.With(expectedSortDirection, currentIndex, currentItem).For(actual, customMessage, actualExpression);
            }

            previousItem = currentItem;
        }
    }

    /// <summary>
    /// Asserts that the elements in the enumerable are of the specified types.
    /// </summary>
    public static void ShouldBeOfTypes<T>(this IEnumerable<T> actual, Type[] expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.Select(x => x!.GetType()).ToArray().ShouldBe(expected, customMessage, actualExpression: actualExpression);
    }
}