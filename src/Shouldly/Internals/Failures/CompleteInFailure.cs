namespace Shouldly.Internals.Failures;

/// <summary>
/// A task or delegate should have completed within a timeout.
/// </summary>
sealed class CompleteInFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// What was running, and how long it was given.
    /// </summary>
    public FailureMessage With(string what, TimeSpan timeout) =>
        new(this, sentence => Word(sentence, what, timeout));

    internal static string Word(Sentence sentence, object? expectedValue, TimeSpan? timeout) =>
        $"""

         {expectedValue}
             should complete in
         {timeout}
             but did not
         """;
}
