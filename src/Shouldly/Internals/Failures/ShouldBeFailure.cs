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
        MessageShape.IgnoringOrder => new(IgnoringOrder(s.Expected, null), s.Actual),
        MessageShape.Tolerance when s.Tolerance is { } tolerance => new(Within(s.Expected, tolerance), s.Actual),
        MessageShape.CaseSensitivity when s.CaseSensitivity is { } caseSensitivity => new(ComparedSomehow(s.Expected, caseSensitivity), s.Actual),
        _ => new(ComparedSomehow(s.Expected, caseSensitivity: null), s.Actual)
    };

    // A tolerance has its own wording. A case option is only shown for a sequence of strings.
    protected override bool ShowsOption(Slots s) =>
        s.Shape == MessageShape.Tolerance || SaysHowCaseWasTreated(s.Actual, s.Expected);

    /// <summary>
    /// The value it was compared with, and what the assertion established comparing them when that is more than
    /// Shouldly's own equality.
    /// </summary>
    public FailureMessage With(object? expected, ComparisonRecord? comparison = null) =>
        new(this, sentence => WordEquality(sentence, expected, comparison, caseSensitivity: null));

    /// <summary>
    /// A sequence of strings it was compared with, under a case option.
    /// </summary>
    public FailureMessage With(IEnumerable<string> expected, Case caseSensitivity) =>
        new(this, sentence => WordEquality(sentence, expected, ComparedStringsWith(caseSensitivity), caseSensitivity));

    // So that strings differing only in case aren't marked as differences when case was ignored.
    private static ComparisonRecord? ComparedStringsWith(Case caseSensitivity) =>
        caseSensitivity == Case.Insensitive
            ? ComparisonRecord.ComparedElementsWith<string>(StringComparer.OrdinalIgnoreCase, isCustom: false)
            : null;

    // A custom assertion states its values but not how it compared them, so its message can't say why they differ.
    private FailureMessage ComparedSomehow(object? expected, Case? caseSensitivity) =>
        new(this, sentence => WordEquality(sentence, expected, record: null, caseSensitivity, explain: false));

    /// <summary>
    /// The value it should have been within a tolerance of.
    /// </summary>
    public FailureMessage Within(object? expected, object tolerance) =>
        new(this, sentence => WithinWording.Word(sentence, expected, tolerance));

    /// <summary>
    /// The sequence it should have matched in any order, and how the assertion compared their elements.
    /// </summary>
    public FailureMessage IgnoringOrder(object? expected, ComparisonRecord? comparison, Case? caseSensitivity = null) =>
        new(this, sentence => IsWalkable(sentence.Actual) && IsWalkable(expected)
            ? IgnoringOrderWording.Word(sentence, expected, comparison, caseSensitivity)
            : Word(sentence, expected, comparison));

    /// <summary>
    /// The string it was compared with under string comparison options.
    /// </summary>
    public FailureMessage With(ComparedStrings strings) =>
        new(this, sentence => StringWording.Word(sentence, strings));

    // A sequence of strings that should have equalled another says how case was treated.
    private string WordEquality(Sentence sentence, object? expected, ComparisonRecord? record, Case? caseSensitivity, bool explain = true) =>
        SaysHowCaseWasTreated(sentence.Actual, expected)
            ? StringSequenceWording.Word(sentence, expected, record, caseSensitivity, explain)
            : Word(sentence, expected, record, explain);

    private bool SaysHowCaseWasTreated(object? actual, object? expected) =>
        !Negated && actual is not null && CapturedSequence.Original(actual) is IEnumerable<string> && expected is not Expression;

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated; it is reported as null (#1406).
    private static bool IsWalkable(object? value) =>
        value is IEnumerable && !NullEquivalence.IsNull(value);

    internal static string Word(Sentence sentence, object? expectedValue, ComparisonRecord? record, bool explain = true)
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
            actual = (record ?? ComparisonRecord.Default).UsesCustomComparer
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
            : message + DifferenceAndReason(sentence.Actual, expectedValue, record, explain);
    }

    /// <summary>
    /// What closes the message of a value that should have equalled another: the differences between two sequences,
    /// and, with <paramref name="explain"/>, the line that says why the two are unequal when the message doesn't
    /// otherwise show it.
    /// </summary>
    internal static string DifferenceAndReason(object? actual, object? expected, ComparisonRecord? record, bool explain)
    {
        var found = DifferenceHighlighter.Find(actual, expected, record);
        var closing = string.Empty;

        // A list with nothing marked in it would only repeat the actual value.
        if (found is { FirstIndex: not null } difference)
        {
            closing += $"""

                            difference
                        {difference.Text}
                        """;
        }

        if (explain && Inequality.Explain(actual, expected, record, found) is { } reason)
        {
            closing += $"""

                            {reason}
                        """;
        }

        return closing;
    }
}
