using ExpressionToString;

namespace Shouldly.Internals.Failures;

/// <summary>
/// How a failure to contain, or not contain, an element satisfying a condition is worded.
/// </summary>
static class ContainConditionWording
{
    internal static string Word(Sentence sentence, object? expectedValue, Expression? condition, int? matchCount)
    {
        var subject = sentence.Subject == "null" ? sentence.ActualText : sentence.Subject;

        var elementPhrase = matchCount.HasValue
            ? matchCount.Value + " element(s)"
            : "an element";

        var should = sentence.Phrase;
        if (sentence.Negated && condition is { } filter)
        {
            return
                $"""
                 {subject}
                     {should} {elementPhrase} satisfying the condition
                 {ExpressionStringBuilder.ToString(filter)}
                     but
                 {expectedValue.ToStringAwesomely()}
                     do
                 """;
        }

        var expected = expectedValue.ToStringAwesomely();
        if (sentence.Negated)
        {
            return
                $"""
                 {subject}
                     {should} {elementPhrase} satisfying the condition
                 {expected}
                     but does{""}
                 """;
        }

        return
            $"""
             {subject}
                 {should} {elementPhrase} satisfying the condition
             {expected}
                 but does not
             """;
    }
}
