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
        var missing = UnmatchedItems(context).Cast<object>().ToList();

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

    // ShouldBeSupersetOf passes the items it found missing, so any custom comparer is honored. Recompute with default equality for other callers.
    private static IEnumerable UnmatchedItems(IShouldlyAssertionContext context)
    {
        if (context is ShouldlyAssertionContext { UnmatchedItems: { } unmatchedItems })
            return unmatchedItems;

        var actualEnumerable = (context.Actual as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();
        var expectedEnumerable = (context.Expected as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();
        return expectedEnumerable.Except(actualEnumerable);
    }
}