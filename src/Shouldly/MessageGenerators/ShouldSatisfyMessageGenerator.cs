namespace Shouldly.MessageGenerators;

// Handles the static Should.Satisfy(...) entry point, which has no value under test and therefore
// no code part / subject. The subject-based ShouldSatisfy<T> extension uses
// ShouldSatisfyAllConditionsMessageGenerator instead.
class ShouldSatisfyMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("^Satisfy$");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context) =>
        $"""
         The conditions specified should all be satisfied, but were not.
         The following errors were found ...
         {context.Expected}
         """;
}
