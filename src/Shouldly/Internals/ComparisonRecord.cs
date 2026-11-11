namespace Shouldly.Internals;

/// <summary>
/// How an assertion compared its values, when that was by something other than Shouldly's own equality. A failure
/// message reads it so that what it reports agrees with the comparison that failed: items a custom comparer
/// matched aren't highlighted as differences, and a custom comparer is not described as case sensitive.
/// </summary>
sealed class ComparisonRecord
{
    private readonly Func<object?, object?, bool>? _elementEquals;

    private ComparisonRecord(Func<object?, object?, bool>? elementEquals, bool usesCustomComparer)
    {
        _elementEquals = elementEquals;
        UsesCustomComparer = usesCustomComparer;
    }

    /// <summary>
    /// The record of an assertion that compared with Shouldly's own equality.
    /// </summary>
    public static ComparisonRecord Default { get; } = new(null, usesCustomComparer: false);

    /// <summary>
    /// Whether the caller supplied the comparer, so the comparison's rules are theirs and not Shouldly's.
    /// </summary>
    public bool UsesCustomComparer { get; }

    /// <summary>
    /// Whether two elements are equal as the assertion compared them.
    /// </summary>
    public bool ElementsEqual(object? actual, object? expected) =>
        _elementEquals is { } elementEquals
            ? elementEquals(actual, expected)
            : Is.Equal(actual, expected);

    /// <summary>
    /// The assertion compared the elements of two sequences with <paramref name="comparer"/>.
    /// </summary>
    /// <param name="comparer">The comparer the elements were compared with.</param>
    /// <param name="isCustom">Whether the caller supplied it.</param>
    public static ComparisonRecord ComparedElementsWith<T>(IEqualityComparer<T> comparer, bool isCustom) =>
        new((actual, expected) => Is.Equal((T?)actual, (T?)expected, comparer), isCustom);

    /// <summary>
    /// The assertion compared two values with a comparer the caller supplied.
    /// </summary>
    public static ComparisonRecord ComparedValuesWithCustomComparer() =>
        new(null, usesCustomComparer: true);
}
