namespace Shouldly.Internals.Failures;

/// <summary>
/// A set of conditions with no single subject should all have been satisfied.
/// </summary>
sealed class SatisfyFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected)), s.Actual);

    /// <summary>
    /// The failures of the conditions that were not satisfied.
    /// </summary>
    public FailureMessage With(string errors) =>
        new(this, sentence => Word(sentence, errors));

    internal static string Word(Sentence sentence, object? expectedValue) =>
        $"""
         The conditions specified should all be satisfied, but were not.
         The following errors were found ...
         {expectedValue}
         """;
}
