namespace Shouldly.MessageGenerators;

class ShouldBeNegativeMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBeNegative");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actual = context.Actual.ToStringAwesomely();
        var actualValue = codePart != actual ? $"""

                                                {actual}
                                                    
                                                """ : " ";

        var should = context.ShouldMethod.PascalToSpaced();
        return
            $"""
             {codePart}
                 {should} but{actualValue}is positive
             """;
    }
}