namespace Shouldly.Internals.Failures;

/// <summary>
/// A dictionary should have contained a key, or should not have.
/// </summary>
sealed class KeyFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    public FailureMessage With(object? key) =>
        new(this, sentence => Word(sentence, key));

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        const string format =
            """
            {0}
                {1}
            {2}
                but does{3}
            """;

        var codePart = sentence.Subject ?? sentence.ActualText;
        var expected = expectedValue.ToStringAwesomely();

        var should = sentence.Phrase;
        if (sentence.Negated)
        {
            return string.Format(format, codePart, should, expected, "");
        }

        return string.Format(format, codePart, should, expected, " not");
    }
}
