namespace Shouldly.Internals.Failures;

/// <summary>
/// Words the failure of a custom assertion that states it through one of the public message classes. An assertion
/// named exactly like a built-in one gets that assertion's wording, unless that wording would drop a tolerance or
/// case option; any other gets the plain shape of the class it constructed.
/// </summary>
static class CustomAssertionMessage
{
    private static readonly Regex NotWord = new(@"\bnot\b", RegexOptions.IgnoreCase);

    public static string Text(string shouldlyMethod, Slots slots, string? customMessage, string? actualExpression)
    {
        // The message reads a sequence more than once: to format it, to stand in for the subject and to highlight
        // differences. Some wordings also read all of it, to count it or to find the items outside another, so a
        // sequence is captured as it is read rather than cut to what the message shows.
        var expected = CapturedSequence.Lazily(slots.Expected);
        var actual = CapturedSequence.Lazily(slots.Actual);
        try
        {
            slots = slots.WithValues(expected, actual);
            var reading = FailureNames.Find(shouldlyMethod)?.Read(slots) ?? PlainShapeOf(shouldlyMethod, slots);
            return reading.Message.Text(reading.Actual, customMessage, actualExpression);
        }
        finally
        {
            CapturedSequence.Release(expected);
            CapturedSequence.Release(actual);
        }
    }

    /// <summary>
    /// Whether a phrase says something should not hold, as <c>should not be empty</c> does.
    /// </summary>
    public static bool IsNegated(string phrase) => NotWord.IsMatch(phrase);

    private static SlotReading PlainShapeOf(string shouldlyMethod, Slots slots)
    {
        var phrase = shouldlyMethod.PascalToSpaced();
        var negated = IsNegated(phrase);

        Func<Sentence, string> wording;
        switch (slots.Shape)
        {
            case MessageShape.Expected:
                wording = sentence => ExpectedOnlyFailure.Word(sentence, slots.Expected);
                break;
            case MessageShape.Tolerance:
                wording = sentence => WithinWording.Word(sentence, slots.Expected, slots.Tolerance, WithinWording.ImpliedBy(slots.Tolerance));
                break;
            case MessageShape.CaseSensitivity when slots.CaseSensitivity is { } caseSensitivity:
                phrase += caseSensitivity == Case.Insensitive ? " (case insensitive comparison)" : " (case sensitive comparison)";
                wording = sentence => PlainFailure.Word(sentence, slots.Expected);
                break;
            case MessageShape.IgnoringOrder when IsWalkable(slots.Actual) && IsWalkable(slots.Expected):
                wording = sentence => IgnoringOrderWording.Word(sentence, slots.Expected, Equality.OfSequences(Equality<object?>.Default, ignoreOrder: true));
                break;
            case MessageShape.Throw when negated:
                wording = sentence => NotThrowFailure.Word(sentence, slots.Expected, slots.Actual as Exception, slots.ExceptionMessage, isTask: false, extensionForm: false);
                break;
            case MessageShape.Throw:
                wording = sentence => ThrowFailure.Word(sentence, slots.Expected, slots.HasActual ? slots.Actual : null, slots.ExceptionMessage, isTask: false, extensionForm: false, exactly: false);
                break;
            case MessageShape.Equivalence:
                wording = sentence => EquivalentFailure.Word(sentence, slots.Expected, null, slots.Path);
                break;
            default:
                wording = sentence => PlainFailure.Word(sentence, slots.Expected);
                break;
        }

        return new(new(shouldlyMethod, phrase, negated, wording), slots.Actual);
    }

    // A default ImmutableArray<T> is IEnumerable but throws when enumerated; it is reported as null (#1406).
    private static bool IsWalkable(object? value) =>
        value is IEnumerable && !NullEquivalence.IsNull(value);
}
