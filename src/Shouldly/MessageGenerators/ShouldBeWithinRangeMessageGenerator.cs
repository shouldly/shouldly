using Shouldly.DifferenceHighlighting;
using System;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeWithinRangeMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return (context.ShouldMethod.StartsWith("ShouldBe", StringComparison.Ordinal) || context.ShouldMethod.StartsWith("ShouldNotBe", StringComparison.Ordinal))
                   && !context.ShouldMethod.Contains("Contain")
                   && context.Tolerance != null;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var message = GetMessage(context);

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                message += $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
            }

            return message;
        }

        private string GetMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var tolerance = context.Tolerance.ToStringAwesomely();
            var expected = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();

            string actual = codePart == actualValue ? " not" : $@"
{actualValue}";

            var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

            var message = $@"{codePart}
    should {negated}be within
{tolerance}
    of
{expected}
    but was{actual}";

            if (context.Expected is TimeSpan span && context.Tolerance != null)
            {
                var difference = (span - (TimeSpan)context.Tolerance).ToStringAwesomely();
                if (codePart != actualValue)
                {
                    codePart = $"{codePart} ({actualValue})";
                }

                message = $@"{codePart}
    should {negated}be within
{tolerance}
    of
{expected}
    but had a difference of
{difference}";
            }

            return message;
        }
    }
}