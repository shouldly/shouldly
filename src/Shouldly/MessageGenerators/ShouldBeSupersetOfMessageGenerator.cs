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
        var actualEnumerable = (context.Actual as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();
        var expectedEnumerable = (context.Expected as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();

        var extra = expectedEnumerable.Except(actualEnumerable).ToList();
        var count = extra.Count;

        return
            $"""
             {codePart}
                 should be superset of
             {expected}
                 but
             {extra.ToStringAwesomely()}
                 {(count > 1 ? "are" : "is")} outside superset
             """;
    }
}