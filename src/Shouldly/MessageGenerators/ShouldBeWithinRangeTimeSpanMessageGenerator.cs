using Shouldly.DifferenceHighlighting;
using System;

namespace Shouldly.MessageGenerators;

internal class ShouldBeWithinRangeTimeSpanMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return
            context.Expected is TimeSpan &&
            (context.ShouldMethod.StartsWith("ShouldBe", StringComparison.Ordinal) ||
             context.ShouldMethod.StartsWith("ShouldNotBe", StringComparison.Ordinal))
            && !context.ShouldMethod.Contains("Contain")
            && context.Tolerance != null;
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var tolerance = context.Tolerance.ToStringAwesomely();
        var expected = context.Expected.ToStringAwesomely();
        var actual = context.Actual.ToStringAwesomely();
        var actualValue = (TimeSpan)(context.Actual ?? throw new InvalidOperationException());
        var expectedValue = (TimeSpan)(context.Expected ?? throw new InvalidOperationException());
        var header = codePart != "timeSpan" ? $"{{{codePart}}}" : $"{codePart} {{{actual}}}";
        var difference = (expectedValue - actualValue).ToStringAwesomely();
        var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

        var message =
            $@"{header}
    should {negated}be within
{tolerance}
    of
{expected}
    but had difference of 
{difference}";

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            message += $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
        }

        return message;
    }
}