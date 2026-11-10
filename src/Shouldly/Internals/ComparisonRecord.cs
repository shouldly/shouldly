using Shouldly.Equivalency;

namespace Shouldly.Internals;

/// <summary>
/// What an assertion established while comparing actual with expected: the element equality it used, or a result
/// it already computed. The assertion records this on its context so the failure message reports what the
/// assertion did, rather than redoing the comparison with default equality.
/// </summary>
/// <remarks>
/// Each factory belongs to one family of assertions and records everything that family knows in one call, so
/// related facts (a comparer and whether it is custom) can't be set apart. Message generators don't read this
/// directly; they go through <see cref="AssertionComparison"/>, which supplies the defaults.
/// </remarks>
sealed class ComparisonRecord
{
    private ComparisonRecord(
        Func<object?, object?, bool>? elementEquals = null,
        bool usesCustomComparer = false,
        IEnumerable? unmatchedItems = null,
        DictionaryKeyLookup? keyLookup = null,
        IReadOnlyList<EquivalencyDifference>? equivalencyDifferences = null)
    {
        ElementEquals = elementEquals;
        UsesCustomComparer = usesCustomComparer;
        UnmatchedItems = unmatchedItems;
        KeyLookup = keyLookup;
        EquivalencyDifferences = equivalencyDifferences;
    }

    /// <summary>Called as <c>(actual, expected)</c>.</summary>
    public Func<object?, object?, bool>? ElementEquals { get; }

    public bool UsesCustomComparer { get; }

    public IEnumerable? UnmatchedItems { get; }

    public DictionaryKeyLookup? KeyLookup { get; }

    public IReadOnlyList<EquivalencyDifference>? EquivalencyDifferences { get; }

    /// <summary>
    /// A collection comparison that compared elements with <paramref name="comparer"/>. Pass
    /// <paramref name="isCustom"/> as <c>true</c> when the caller supplied the comparer, since the message then
    /// can't describe its semantics (case sensitivity included).
    /// </summary>
    public static ComparisonRecord ComparedElementsWith<T>(IEqualityComparer<T> comparer, bool isCustom) =>
        new(
            elementEquals: (actual, expected) => Is.Equal((T?)actual, (T?)expected, comparer),
            usesCustomComparer: isCustom);

    /// <summary>
    /// A comparison of whole values with a caller-supplied comparer. Records no element equality: the comparer
    /// compares whole values, so it can't stand in for comparing their elements.
    /// </summary>
    public static ComparisonRecord ComparedValuesWithCustomComparer() =>
        new(usesCustomComparer: true);

    /// <summary>
    /// A subset comparison, with the actual items it found outside the expected set.
    /// </summary>
    public static ComparisonRecord FoundUnmatched(IEnumerable unmatchedItems) =>
        new(unmatchedItems: unmatchedItems);

    /// <summary>
    /// A dictionary key-and-value comparison, with the result of looking the key up through the dictionary's own
    /// comparer.
    /// </summary>
    public static ComparisonRecord LookedUpKey(bool keyExists, object? value) =>
        new(keyLookup: new(keyExists, value));

    /// <summary>
    /// An equivalency comparison, with the differences it collected.
    /// </summary>
    public static ComparisonRecord FoundDifferences(IReadOnlyList<EquivalencyDifference> differences) =>
        new(equivalencyDifferences: differences);
}
