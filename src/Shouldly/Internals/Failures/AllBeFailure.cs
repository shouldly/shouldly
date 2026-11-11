using ExpressionToString;

namespace Shouldly.Internals.Failures;

/// <summary>
/// Every element of a collection should have satisfied a condition.
/// </summary>
sealed class AllBeFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The condition, and the elements that did not satisfy it.
    /// </summary>
    public FailureMessage With(Expression condition, object? failingElements) =>
        new(this, sentence => Word(sentence, failingElements, condition));

    internal static string Word(Sentence sentence, object? expectedValue, Expression filter)
    {
        var codePart = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var expression = ExpressionStringBuilder.ToString(filter);
        return $"""
                {codePart}
                    should satisfy the condition
                {expression}
                    but
                {expected}
                    do not
                """;
    }
}
