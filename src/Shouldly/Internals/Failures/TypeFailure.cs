namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have been of, or assignable to, a type.
/// </summary>
sealed class TypeFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    public FailureMessage With(Type expected) =>
        new(this, sentence => Word(sentence, expected));

    internal static string Word(Sentence sentence, object? expected)
    {
        var actualType = sentence.Actual?.GetType().FullName;

        var actualString = sentence.SubjectShows(actualType) || sentence.Subject == "null" ? " not" : $"""

             {actualType ?? "null"}
             """;

        return
            $"""
             {sentence.Subject}
                 {sentence.Phrase}
             {expected.ToStringAwesomely()}
                 but was{actualString}
             """;
    }
}
