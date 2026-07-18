namespace Shouldly.MessageGenerators;

class ShouldHaveSingleItemMessageGenerator : ShouldlyMessageGenerator
{
    private const string ShouldBeAssertion = "ShouldHaveSingleItem";

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected;
        var should = context.ShouldMethod.PascalToSpaced();

        if (expected is null)
        {
            if (codePart != "null")
            {
                return
                    $"""
                     {codePart}
                         {should} but was
                     null
                     """;
            }

            return
                $"""
                 null
                     {should}
                 """;
        }

        var expectedString = expected.ToStringAwesomely();
        var count = (expected as IEnumerable)?.Cast<object>().Count() ?? 0;

        if (codePart != "null")
        {
            return
                $"""
                 {codePart}
                     {should} but had
                 {count}
                     items and was
                 {expectedString}
                 """;
        }

        return
            $"""
             {expectedString}
                 {should} but had
             {count}
                 items
             """;
    }
}