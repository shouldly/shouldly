namespace Shouldly.MessageGenerators;

class ShouldHaveCountMessageGenerator : ShouldlyMessageGenerator
{
    private const string ShouldHaveCount = "ShouldHaveCount";

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod.Equals(ShouldHaveCount, StringComparison.OrdinalIgnoreCase);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var count = context.Actual switch
        {
            // The assertion always hands us a materialized collection, so read its count cheaply rather than re-enumerating.
            ICollection collection => collection.Count,
            IEnumerable enumerable => enumerable.Cast<object>().Count(),
            _ => 0
        };
        var actual = context.Actual.ToStringAwesomely();
        var expectedItems = expected == "1" ? "item" : "items";
        var actualItems = count == 1 ? "item" : "items";

        if (!context.CodePartMatchesActual)
        {
            return
                $"""
                 {codePart}
                     should have {expected} {expectedItems} but had
                 {count}
                     {actualItems} and was
                 {actual}
                 """;
        }

        return
            $"""
             {codePart}
                 should have {expected} {expectedItems} but had
             {count}
                 {actualItems}
             """;
    }
}
