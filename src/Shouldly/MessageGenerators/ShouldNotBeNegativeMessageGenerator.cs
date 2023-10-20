using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators;

internal class ShouldNotBeNegativeMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldNotBeNegative");

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return Validator.IsMatch(context.ShouldMethod);
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actual = context.Actual.ToStringAwesomely();
        var actualValue = codePart != actual ? $@"
{actual}
    " : " ";

        var should = context.ShouldMethod.PascalToSpaced();
        return
            $@"{codePart}
    {should} but{actualValue}is negative";
    }
}