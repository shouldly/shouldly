namespace Shouldly.MessageGenerators;

class ShouldNotHaveFlagMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod == "ShouldNotHaveFlag";

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();

        var actual = context.Actual.ToStringAwesomely();
        var actualString = codePart == actual ? " had" : $"""
                                                           it had
                                                          {actual}
                                                          """;

        return $"""
                {codePart}
                    should not have flag
                {expected}
                    but{actualString}
                """;
    }
}