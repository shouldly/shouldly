namespace Shouldly.Internals.Failures;

/// <summary>
/// The items of a collection should have been in order.
/// </summary>
sealed class InOrderFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The order required, and the first item found out of it.
    /// </summary>
    public FailureMessage With(SortDirection direction, int outOfOrderIndex, object? outOfOrderItem) =>
        new(this, sentence => Word(sentence, direction, outOfOrderIndex, outOfOrderItem));

    internal static string Word(Sentence sentence, SortDirection direction, int outOfOrderIndex, object? outOfOrderItem) =>
        $"""
         {sentence.Subject}
             should be in {direction.ToString().ToLower()} order but was not.
             The first out-of-order item was found at index {outOfOrderIndex}:
         {outOfOrderItem}
         """;
}
