namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have been the same instance as another.
/// </summary>
sealed class SameAsFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    protected override SlotReading? FromSlots(Slots s) =>
        new(With(s.Expected), s.Actual);

    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected)
    {
        var subject = sentence.Subject;
        var actualValue = sentence.ActualText;
        var actual = subject == actualValue
            ? " not"
            : $"\r\n{actualValue}";

        var message =
            $"""
             {subject}
                 should be same as
             {expected.ToStringAwesomely()}
                 but was{actual}
             """;

        // With a null on either side there are not two instances to tell apart.
        if (sentence.Actual is { } actualInstance && expected is not null)
        {
            var equalByValue = AreEqualByValue(actualInstance, expected)
                ? " (the two are equal by value)"
                : string.Empty;
            message += $"""

                            which is a different instance{equalByValue}
                        """;
        }

        return message;
    }

    // The assertion itself never called Equals, so a throwing Equals must not replace the identity failure.
    private static bool AreEqualByValue(object actual, object expected)
    {
        try
        {
            return Equality<object>.Default.Equal(actual, expected);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
