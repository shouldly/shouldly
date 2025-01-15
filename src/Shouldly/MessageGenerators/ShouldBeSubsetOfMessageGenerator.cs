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
        var actualEnumerable = (context.Actual as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();
        var expectedEnumerable = (context.Expected as IEnumerable ?? Enumerable.Empty<object>()).Cast<object>();

        var missing = actualEnumerable.Except(expectedEnumerable).ToList();
        var count = missing.Count;

        return
            $@"{codePart}
    should be subset of
{expected}
    but
{missing.ToStringAwesomely()}
    {(count > 1 ? "are" : "is")} outside subset";
    }
}