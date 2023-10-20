namespace Shouldly.MessageGenerators;

class ShouldSatisfyAllConditionsMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldSatisfyAllConditions");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected?.ToString();

        return
            $@"{codePart}
    should satisfy all the conditions specified, but does not.
The following errors were found ...
{expected}";
    }
}