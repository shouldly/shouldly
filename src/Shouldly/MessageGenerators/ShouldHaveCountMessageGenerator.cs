namespace Shouldly.MessageGenerators;

class ShouldHaveCountMessageGenerator : ShouldlyMessageGenerator
{
    private const string ShouldBeAssertion = "ShouldHaveCount";

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var count = (context.Actual as IEnumerable)?.Cast<object>().Count() ?? 0;
        var actual = context.Actual.ToStringAwesomely();
        var expectedItems = expected == "1" ? "item" : "items";
        var actualItems = count == 1 ? "item" : "items";

        if (!context.CodePartMatchesActual)
        {
            return
                $@"{codePart}
    should have {expected} {expectedItems} but had
{count}
    {actualItems} and was
{actual}";
        }

        return
            $@"{codePart}
    should have {expected} {expectedItems} but had
{count}
    {actualItems}";
    }
}