namespace Shouldly.MessageGenerators;

class ShouldBeBooleanMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBe(True|False)");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();

        var actual = context.Actual.ToStringAwesomely();
        var actualString = codePart == actual ? " not" : $"""

                                                          {actual}
                                                          """;

        return $"""
                {codePart}
                    should be
                {expected}
                    but was{actualString}
                """;
    }
}