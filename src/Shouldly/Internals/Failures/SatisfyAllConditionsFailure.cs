namespace Shouldly.Internals.Failures;

/// <summary>
/// A value should have satisfied every one of a set of conditions.
/// </summary>
sealed class SatisfyAllConditionsFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The failures of the conditions that were not satisfied.
    /// </summary>
    public FailureMessage With(string errors) =>
        new(this, sentence => Word(sentence, errors));

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        var codePart = sentence.Subject;
        var expected = expectedValue?.ToString();

        return
            $"""
             {codePart}
                 should satisfy all the conditions specified, but does not.
             The following errors were found ...
             {expected}
             """;
    }
}
