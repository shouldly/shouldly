namespace Shouldly.Internals;

/// <summary>
/// Which of Shouldly's own rules compares a pair of values. <see cref="EqualityComparer{T}"/> takes this step to decide
/// whether the two are equal, and a failure message reads the same step to say what decided, so the two can't
/// disagree about how a pair was compared.
/// </summary>
readonly struct EqualityStep
{
    private EqualityStep(EqualityStepKind kind, Type? ruleType = null, bool verdict = false,
        IEnumerable? expectedItems = null, IEnumerable? actualItems = null)
    {
        Kind = kind;
        RuleType = ruleType;
        Verdict = verdict;
        ExpectedItems = expectedItems;
        ActualItems = actualItems;
    }

    public EqualityStepKind Kind { get; }

    /// <summary>The type of the equality rule that compares them, when <see cref="Kind"/> is <see cref="EqualityStepKind.Rule"/>.</summary>
    public Type? RuleType { get; }

    /// <summary>
    /// Whether the rule, or the comparison as JSON, found them equal, when <see cref="Kind"/> is
    /// <see cref="EqualityStepKind.Rule"/> or <see cref="EqualityStepKind.Json"/>.
    /// </summary>
    public bool Verdict { get; }

    /// <summary>The expected value's items, when it is compared as a sequence or an unordered collection.</summary>
    public IEnumerable? ExpectedItems { get; }

    /// <summary>The actual value's items, when it is compared as a sequence or an unordered collection.</summary>
    public IEnumerable? ActualItems { get; }

    /// <summary>
    /// The step that compares two values that are neither null nor the same instance, in the order Shouldly takes
    /// them: an equality rule, then JSON, a string, a number, a collection, a tuple, and last the type's own equality.
    /// </summary>
    public static EqualityStep For(object expected, object actual) =>
        // A registered equality rule decides before anything else, so a value it covers is never walked.
        ByRule(expected, actual) ?? Structural(expected, actual);

    /// <summary>
    /// The step <see cref="For"/> takes first: an equality rule that covers both values, or, when none does, two JSON
    /// values compared as JSON. Null when neither decides, for a comparison that walks the two its own way after.
    /// </summary>
    public static EqualityStep? ByRule(object expected, object actual) =>
        EqualityRules.TryDecide(expected, actual, out var ruleType, out var verdict)
            ? new(ruleType is null ? EqualityStepKind.Json : EqualityStepKind.Rule, ruleType, verdict)
            : null;

    /// <summary>
    /// The step <see cref="For"/> takes once rules and JSON have had their say: what the two values are shaped as.
    /// </summary>
    public static EqualityStep Structural(object expected, object actual)
    {
        // Strings are compared with their own Equals rather than walked as sequences of characters.
        if (expected is string && actual is string)
            return new(EqualityStepKind.String);

        if (Numbers.IsNumber(expected) && Numbers.IsNumber(actual))
            return new(EqualityStepKind.Number);

        // Anything it can enumerate, a Memory<T> included. The order a dictionary or a set enumerates in isn't part
        // of its value (#1430).
        if (expected.TryGetEnumerable(out var expectedItems) && actual.TryGetEnumerable(out var actualItems))
        {
            return new(
                UnorderedCollection.IgnoresOrder(actual, expected) ? EqualityStepKind.UnorderedCollection : EqualityStepKind.Sequence,
                expectedItems: expectedItems,
                actualItems: actualItems);
        }

        // A key/value pair or a tuple is compared item by item, so a dictionary's entries compare like its values do
        // (#1408), and a tuple's items like the items on their own (#1429), not by the tuple's own Equals, which
        // compares a collection by reference.
        if (Tuples.AreComparedItemByItem(expected, actual))
            return new(EqualityStepKind.Tuple);

        return new(EqualityStepKind.OwnEquality);
    }
}

/// <summary>
/// The steps of Shouldly's own rules, in the order it takes them.
/// </summary>
enum EqualityStepKind
{
    /// <summary>An equality rule registered with <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>.</summary>
    Rule,

    /// <summary>Two System.Text.Json values, compared as JSON.</summary>
    Json,

    /// <summary>Two strings, compared ordinally.</summary>
    String,

    /// <summary>Two numbers, compared by value whatever their types.</summary>
    Number,

    /// <summary>Two collections at least one of which is a dictionary or a set: items matched in any order.</summary>
    UnorderedCollection,

    /// <summary>Two sequences: items compared in order.</summary>
    Sequence,

    /// <summary>Two key/value pairs or tuples: items compared one by one.</summary>
    Tuple,

    /// <summary>
    /// The type's own equality: <c>IEquatable&lt;T&gt;</c>, an <c>Equals</c> override, <c>CompareTo</c>, or reference
    /// equality.
    /// </summary>
    OwnEquality
}
