namespace Shouldly.Internals.Failures;

/// <summary>
/// A string should not have matched a pattern.
/// </summary>
sealed class NotMatchFailure([CallerMemberName] string name = "") : Failure(name, negated: true)
{
    public FailureMessage With(object? expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        const string format = "{0} should not match {1} but did";

        var codePart = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();

        return string.Format(format, codePart, expected);
    }
}
