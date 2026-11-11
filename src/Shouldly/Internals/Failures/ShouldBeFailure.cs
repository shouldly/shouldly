using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have equalled another, or should not have.
/// </summary>
sealed class ShouldBeFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    /// <summary>
    /// The value it was compared with, and what the assertion established comparing them when that is more than
    /// Shouldly's own equality.
    /// </summary>
    public FailureMessage With(object? expected, ComparisonRecord? comparison = null) =>
        new(this, sentence => WordEquality(sentence, expected, comparison, caseSensitivity: null));

    /// <summary>
    /// A sequence of strings it was compared with, under a case option.
    /// </summary>
    public FailureMessage With(object? expected, Case caseSensitivity) =>
        new(this, sentence => WordEquality(sentence, expected, record: null, caseSensitivity));

    /// <summary>
    /// The value it should have been within a tolerance of.
    /// </summary>
    public FailureMessage Within(object? expected, object tolerance) =>
        new(this, sentence => WithinWording.Word(sentence, expected, tolerance));

    /// <summary>
    /// The sequence it should have matched in any order, and how the assertion compared their elements.
    /// </summary>
    public FailureMessage IgnoringOrder(object? expected, ComparisonRecord comparison, Case? caseSensitivity = null) =>
        new(this, sentence => IsWalkable(sentence.Actual) && IsWalkable(expected)
            ? IgnoringOrderWording.Word(sentence, expected, comparison, caseSensitivity)
            : Word(sentence, expected, comparison));

    /// <summary>
    /// The string it was compared with under string comparison options.
    /// </summary>
    public FailureMessage With(ComparedStrings strings) =>
        new(this, sentence => StringWording.Word(sentence, strings));

    // A sequence of strings that should have equalled another says how case was treated.
    private string WordEquality(Sentence sentence, object? expected, ComparisonRecord? record, Case? caseSensitivity) =>
        !Negated && sentence.Actual is IEnumerable<string> && expected is not Expression
            ? StringSequenceWording.Word(sentence, expected, record, caseSensitivity)
            : Word(sentence, expected, record);

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated; it is reported as null (#1406).
    private static bool IsWalkable(object? value) =>
        value is IEnumerable && !NullEquivalence.IsNull(value);

    internal static string Word(Sentence sentence, object? expectedValue, ComparisonRecord? record)
    {
        var codePart = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var actualValue = sentence.ActualText;
        string actual;
        if (sentence.Negated)
        {
            // The values were equal, so repeating actual adds nothing (#304). Equal only by a custom comparer's
            // reckoning is worth saying, though, and so is an actual that reads differently from expected: a value
            // compared by a registered equality rule may be equal whatever its items (#1425).
            actual = AssertionComparison.Of(record).UsesCustomComparer
                ? " (using a custom comparer)"
                : actualValue == expected
                    ? string.Empty
                    : $"\r\n{actualValue}";
        }
        else if (codePart == actualValue)
        {
            actual = " not";
        }
        else
        {
            actual = $"\r\n{actualValue}";
        }

        var message =
            $"""
             {codePart}
                 {sentence.Phrase}
             {expected}
                 but was{actual}
             """;

        // A negated assertion fails only when the values are equal, so there's no difference to highlight.
        if (!sentence.Negated && DifferenceHighlighter.Between(sentence.Actual, expectedValue, record) is { } difference)
        {
            message += $"""
                        
                            difference
                        {difference}
                        """;
        }

        return message;
    }
}
