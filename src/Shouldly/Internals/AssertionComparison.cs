using Shouldly.Equivalency;

namespace Shouldly.Internals;

/// <summary>
/// How a failed assertion compared actual with expected, as message generators and difference highlighters
/// should report it. Uses the <see cref="ComparisonRecord"/> the assertion left on its context where there is one,
/// and otherwise works it out from the public context with default equality: the context may come from a
/// third-party assertion, or from a Shouldly assertion that recorded nothing because default equality is what it
/// used.
/// </summary>
sealed class AssertionComparison
{
    private readonly IShouldlyAssertionContext context;
    private readonly ComparisonRecord? record;

    private AssertionComparison(IShouldlyAssertionContext context, ComparisonRecord? record)
    {
        this.context = context;
        this.record = record;
    }

    /// <summary>
    /// What an assertion established, read from the record alone.
    /// </summary>
    public static AssertionComparison Of(ComparisonRecord? record) =>
        new(null!, record);

    public static AssertionComparison Of(IShouldlyAssertionContext context) =>
        new(context, (context as ShouldlyAssertionContext)?.Comparison);

    /// <summary>
    /// Whether the assertion counted <paramref name="actual"/> and <paramref name="expected"/> as equal elements.
    /// </summary>
    public bool ElementsEqual(object? actual, object? expected) =>
        record?.ElementEquals is { } elementEquals
            ? elementEquals(actual, expected)
            : Is.Equal(actual, expected);

    /// <summary>
    /// Whether the assertion compared with a caller-supplied comparer, whose semantics (case sensitivity included)
    /// the message can't describe.
    /// </summary>
    public bool UsesCustomComparer => record?.UsesCustomComparer ?? false;

    /// <summary>
    /// The actual items that are not in the expected set.
    /// </summary>
    public IEnumerable<object> UnmatchedItems =>
        record?.UnmatchedItems?.Cast<object>()
        ?? AsObjects(context.Actual).Except(AsObjects(context.Expected));

    /// <summary>
    /// Whether the actual dictionary has <see cref="IShouldlyAssertionContext.Key"/>, and its value there; or
    /// <c>null</c> when that can't be worked out. Pass <paramref name="readValue"/> as <c>false</c> when only
    /// <see cref="DictionaryKeyLookup.KeyExists"/> is needed, so the fallback doesn't call the dictionary's indexer.
    /// </summary>
    public DictionaryKeyLookup? KeyLookup(bool readValue = true) =>
        record?.KeyLookup ?? context switch
        {
            { Actual: IDictionary dictionary, Key: { } key } when dictionary.Contains(key) =>
                new(true, readValue ? dictionary[key] : null),
            { Actual: IDictionary } => new(false, null),
            _ => null
        };

    /// <summary>
    /// The differences an equivalency comparison collected; empty when it didn't collect any.
    /// </summary>
    public IReadOnlyList<EquivalencyDifference> EquivalencyDifferences =>
        record?.EquivalencyDifferences ?? [];

    private static IEnumerable<object> AsObjects(object? value) =>
        (value as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();
}
