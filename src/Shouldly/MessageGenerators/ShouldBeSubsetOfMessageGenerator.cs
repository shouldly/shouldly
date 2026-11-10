using Shouldly.Internals;

namespace Shouldly.MessageGenerators;

class ShouldBeSubsetOfMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBeSubsetOf");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        // The items the assertion found outside the subset, so any custom comparer is honored.
        var missing = AssertionComparison.Of(context).UnmatchedItems.ToList();

        return
            $"""
             {codePart}
                 should be subset of
             {expected}
                 but
             {missing.ToStringAwesomely()}
                 {(missing.Count > 1 ? "are" : "is")} outside subset
             """;
    }
}