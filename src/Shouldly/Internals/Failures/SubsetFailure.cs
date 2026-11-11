namespace Shouldly.Internals.Failures;

/// <summary>
/// A collection should have been a subset of another.
/// </summary>
sealed class SubsetFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    // A custom assertion has no record of what fell outside the subset, so it is worked out again.
    protected override SlotReading? FromSlots(Slots s) =>
        new(With(s.Expected, AsObjects(s.Actual).Except(AsObjects(s.Expected)).ToList()), s.Actual);

    private static IEnumerable<object> AsObjects(object? value) =>
        (value as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();

    /// <summary>
    /// The collection it should have been a subset of, and the items the assertion found outside it.
    /// </summary>
    public FailureMessage With(object? expected, IEnumerable outside) =>
        new(this, sentence => Word(sentence, expected, outside));

    internal static string Word(Sentence sentence, object? expectedValue, IEnumerable outside)
    {
        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        // The items the assertion found outside the subset, so any custom comparer is honored.
        var missing = outside.Cast<object>().ToList();

        return
            $"""
             {subject}
                 should be subset of
             {expected}
                 but
             {missing.ToStringAwesomely()}
                 {(missing.Count > 1 ? "are" : "is")} outside subset
             """;
    }
}
