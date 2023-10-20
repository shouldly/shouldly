namespace Shouldly.MessageGenerators;

class ShouldHaveSingleItemMessageGenerator : ShouldlyMessageGenerator
{
    private const string ShouldBeAssertion = "ShouldHaveSingleItem";

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase);
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var count = (context.Expected as IEnumerable)?.Cast<object>().Count() ?? 0;
        var should = context.ShouldMethod.PascalToSpaced();
        if (codePart != "null")
        {
            return
                $@"{codePart}
    {should} but had
{count}
    items and was
{expected}";
        }

        return
            $@"{expected}
    {should} but had
{count}
    items";
    }
}