namespace Shouldly.MessageGenerators;

class ShouldBeSupersetOfMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBeSupersetOf");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();

        // The extension method computes the missing elements (honoring any custom comparer) and passes them as the actual value, so no recomputation happens here.
        var missing = (context.Actual as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>().ToList();

        return
            $"""
             {codePart}
                 should be superset of
             {expected}
                 but
             {missing.ToStringAwesomely()}
                 {(missing.Count > 1 ? "are" : "is")} missing
             """;
    }
}
