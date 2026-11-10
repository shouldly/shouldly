using Shouldly.DifferenceHighlighting;
using Shouldly.Internals;

namespace Shouldly.MessageGenerators;

class ShouldBeMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod is "ShouldBe" or "ShouldNotBe";

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var actualValue = context.Actual.ToStringAwesomely();
        string actual;
        if (context.IsNegatedAssertion)
        {
            // The values were equal, so repeating actual adds nothing (#304). Equal only by a custom comparer's
            // reckoning is worth saying, though, and so is an actual that reads differently from expected: a type
            // registered in CompareAsObjectTypes is equal by its own Equals, whatever its items (#1425).
            actual = AssertionComparison.Of(context).UsesCustomComparer
                ? " (using a custom comparer)"
                : actualValue == expected
                    ? string.Empty
                    : $"\r\n{actualValue}";
        }
        else if (codePart == actualValue)
        {
            actual = " not";
        }
        else
        {
            actual = $"\r\n{actualValue}";
        }

        var message =
            $"""
             {codePart}
                 {context.ShouldMethod.PascalToSpaced()}
             {expected}
                 but was{actual}
             """;

        // A negated assertion fails only when the values are equal, so there's no difference to highlight.
        if (!context.IsNegatedAssertion && DifferenceHighlighter.CanHighlightDifferences(context))
        {
            message += $"""
                        
                            difference
                        {DifferenceHighlighter.HighlightDifferences(context)}
                        """;
        }

        return message;
    }
}