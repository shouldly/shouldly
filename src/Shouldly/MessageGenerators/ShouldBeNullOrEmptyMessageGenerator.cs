namespace Shouldly.MessageGenerators;

class ShouldBeNullOrEmptyMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("Should(Not)?BeNullOrEmpty");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var should = context.ShouldMethod.PascalToSpaced();
        var actual = context.Actual.ToStringAwesomely();

        // A non-empty collection fails ShouldBeNullOrEmpty; report how many items it had, as ShouldBeEmpty does.
        if (!context.IsNegatedAssertion && context.Actual is IEnumerable enumerable and not string)
        {
            var count = enumerable.Cast<object?>().Count();
            return
                $"""
                 {codePart}
                     {should} but had
                 {count}
                     item{(count == 1 ? string.Empty : "s")} and was
                 {actual}
                 """;
        }

        var format = context.CodePartMatchesActual ?
            """
            {0}
                {1}
            """
            :
            """
            {0} ({2})
                {1}
            """;

        return string.Format(format, codePart, should, actual);
    }
}
