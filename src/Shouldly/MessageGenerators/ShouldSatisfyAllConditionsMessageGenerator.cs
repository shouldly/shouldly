namespace Shouldly.MessageGenerators;

class ShouldSatisfyAllConditionsMessageGenerator : ShouldlyMessageGenerator
{
    // Matches both the primary ShouldSatisfy and the obsolete ShouldSatisfyAllConditions overloads
    // (the latter delegates into ShouldSatisfy, so the captured ShouldMethod is "ShouldSatisfy").
    private static readonly Regex Validator = new("ShouldSatisfy");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected?.ToString();

        return
            $"""
             {codePart}
                 should satisfy all the conditions specified, but does not.
             The following errors were found ...
             {expected}
             """;
    }
}