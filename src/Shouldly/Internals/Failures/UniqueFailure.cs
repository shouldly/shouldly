namespace Shouldly.Internals.Failures;

/// <summary>
/// The items of a collection should have been unique.
/// </summary>
sealed class UniqueFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The items that occurred more than once.
    /// </summary>
    public FailureMessage With(object? duplicates) =>
        new(this, sentence => Word(sentence, duplicates));

    internal static string Word(Sentence sentence, object? duplicates)
    {
        var codePart = sentence.Subject;
        var actual = duplicates.ToStringAwesomely();

        if (codePart == actual)
        {
            codePart = sentence.ActualText;
        }

        return
            $"""
             {codePart}
                 {sentence.Phrase} but
             {actual}
                 was duplicated
             """;
    }
}
