using Shouldly.DifferenceHighlighting;
using Shouldly.Internals;

namespace Shouldly.MessageGenerators;

class ShouldBeEnumerableCaseSensitiveMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context is
        {
            ShouldMethod: "ShouldBe",
            // Order-insensitive comparisons belong to ShouldBeIgnoringOrderMessageGenerator, which reports what's
            // missing rather than a positional diff.
            IgnoreOrder: false,
            Expected: not Expression,
            Actual: IEnumerable<string>,
        };

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        // A custom comparer decides case sensitivity itself, so there's nothing accurate to say about it.
        var caseSensitivity = AssertionComparison.Of(context).UsesCustomComparer
            ? string.Empty
            : context.CaseSensitivity == Case.Insensitive
                ? " (case insensitive comparison)"
                : " (case sensitive comparison)";
        var actualValue = context.Actual.ToStringAwesomely();
        string actual;
        if (context.IsNegatedAssertion)
        {
            actual = string.Empty;
        }
        else if (codePart == actualValue)
        {
            actual = $" not{caseSensitivity}";
            caseSensitivity = string.Empty;
        }
        else
        {
            actual = $"\r\n{actualValue}";
        }

        var expected = context.Expected.ToStringAwesomely();
        var format =
            $"""
             {codePart}
                 {context.ShouldMethod.PascalToSpaced()}
             {expected}
                 but was{caseSensitivity}{actual}
             """;

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            format +=
                $"""
                 
                     difference
                 {DifferenceHighlighter.HighlightDifferences(context)}
                 """;
        }

        return format;
    }
}