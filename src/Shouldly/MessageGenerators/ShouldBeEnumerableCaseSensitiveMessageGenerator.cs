using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators;

class ShouldBeEnumerableCaseSensitiveMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context is
        {
            ShouldMethod: "ShouldBe",
            Expected: not Expression,
            Actual: IEnumerable<string>,
        };

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var caseSensitivity = context.CaseSensitivity == Case.Insensitive ? " (case insensitive comparison)" : " (case sensitive comparison)";
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
            $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{expected}
    but was{caseSensitivity}{actual}";

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            format +=
                $@"
    difference
{DifferenceHighlighter.HighlightDifferences(context)}";
        }

        return format;
    }
}