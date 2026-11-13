using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have equalled another, or should not have.
/// </summary>
sealed class ShouldBeFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    protected override SlotReading? FromSlots(Slots s) => s.Shape switch
    {
        MessageShape.Throw or MessageShape.Equivalence => null,
        MessageShape.IgnoringOrder => new(With(s.Expected, Equality.OfSequences(Equality<object?>.Default, ignoreOrder: true)), s.Actual),
        MessageShape.Tolerance when s.Tolerance is { } tolerance => new(Within(s.Expected, tolerance, WithinWording.ImpliedBy(tolerance)), s.Actual),
        MessageShape.CaseSensitivity when s.CaseSensitivity is { } caseSensitivity => new(ValuesOnly(s.Expected, caseSensitivity), s.Actual),
        _ => new(ValuesOnly(s.Expected, caseSensitivity: null), s.Actual)
    };

    // A tolerance has its own wording. A case option is only shown for a sequence of strings.
    protected override bool ShowsOption(Slots s) =>
        s.Shape == MessageShape.Tolerance || SaysHowCaseWasTreated(s.Actual, s.Expected);

    /// <summary>
    /// The value it was compared with, and how the assertion compared them. The comparison picks the wording: within
    /// its tolerance, ignoring order, or plain, saying how case was treated for a sequence of strings.
    /// </summary>
    public FailureMessage With(object? expected, Equality equality) =>
        equality.Tolerance is { } tolerance
            ? Within(expected, tolerance, equality)
            : equality.IgnoresOrder
                ? IgnoringOrder(expected, equality)
                : new(this, sentence => WordEquality(sentence, expected, equality, equality.Case));

    /// <summary>
    /// The string it was compared with under string comparison options.
    /// </summary>
    public FailureMessage With(ComparedStrings strings) =>
        new(this, sentence => StringWording.Word(sentence, strings));

    // A custom assertion states its values but not how it compared them, so its message can't say why they differ.
    private FailureMessage ValuesOnly(object? expected, Case? caseSensitivity) =>
        new(this, sentence => WordEquality(sentence, expected, equality: null, caseSensitivity));

    private FailureMessage Within(object? expected, object tolerance, Equality? equality) =>
        new(this, sentence => WithinWording.Word(sentence, expected, tolerance, equality));

    // Dictionaries and sets are never compared in order, so they are told apart as ShouldBe without ignoreOrder tells
    // them apart: a dictionary by key.
    private FailureMessage IgnoringOrder(object? expected, Equality equality) =>
        new(this, sentence =>
            !IsWalkable(sentence.Actual) || !IsWalkable(expected)
                ? Word(sentence, expected, equality)
                : !UnorderedCollection.IgnoresOrder(sentence.Actual, expected)
                    ? IgnoringOrderWording.Word(sentence, expected, equality)
                    : WordEquality(sentence, expected, equality, equality.Case));

    // A sequence of strings that should have equalled another says how case was treated.
    private string WordEquality(Sentence sentence, object? expected, Equality? equality, Case? caseSensitivity) =>
        SaysHowCaseWasTreated(sentence.Actual, expected)
            ? StringSequenceWording.Word(sentence, expected, equality, caseSensitivity)
            : Word(sentence, expected, equality);

    private bool SaysHowCaseWasTreated(object? actual, object? expected) =>
        !Negated && actual is not null && CapturedSequence.Original(actual) is IEnumerable<string> && expected is not Expression;

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated; it is reported as null (#1406).
    private static bool IsWalkable(object? value) =>
        value is IEnumerable && !NullEquivalence.IsNull(value);

    /// <param name="sentence">The sentence the message is written for.</param>
    /// <param name="expectedValue">The value the actual value should have equalled.</param>
    /// <param name="equality">How the assertion compared them, or null when that isn't known, as for a custom assertion.</param>
    internal static string Word(Sentence sentence, object? expectedValue, Equality? equality)
    {
        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var actualValue = sentence.ActualText;
        string actual;
        if (sentence.Negated)
        {
            // The values were equal, so repeating actual adds nothing (#304). Equal only by a custom comparer's
            // reckoning is worth saying, though, and so is an actual that reads differently from expected: a value
            // compared by a registered equality rule may be equal whatever its items (#1425).
            actual = equality is { UsesCustomComparer: true }
                ? " (using a custom comparer)"
                : actualValue == expected
                    ? string.Empty
                    : $"\r\n{actualValue}";
        }
        else if (subject == actualValue)
        {
            actual = " not";
        }
        else
        {
            actual = $"\r\n{actualValue}";
        }

        var message =
            $"""
             {subject}
                 {sentence.Phrase}
             {expected}
                 but was{actual}
             """;

        // A negated assertion fails only when the values are equal, so there's no difference to highlight or explain.
        return sentence.Negated
            ? message
            : message + DifferenceAndReason(sentence.Actual, expectedValue, equality);
    }

    /// <summary>
    /// What closes the message of a value that should have equalled another: the differences between two sequences,
    /// and, when how the assertion compared them is known, the line that says why the two are unequal when the
    /// message doesn't otherwise show it.
    /// </summary>
    internal static string DifferenceAndReason(object? actual, object? expected, Equality? equality)
    {
        var found = DifferenceHighlighter.Find(actual, expected, equality);
        var closing = UnorderedWording.Difference(actual, expected, equality) ?? string.Empty;

        // A list with nothing marked in it would only repeat the actual value.
        if (found is { FirstIndex: not null } difference)
        {
            closing += $"""

                            difference
                        {difference.Text}
                        """;
        }

        if (equality is not null && Inequality.Explain(actual, expected, equality, found) is { } reason)
        {
            closing += $"""

                            {reason}
                        """;
        }

        return closing;
    }
}
