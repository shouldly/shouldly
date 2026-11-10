using Shouldly.Internals;

namespace Shouldly.MessageGenerators;

class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("Should(Not)?BeEmpty");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod) && !context.HasRelevantActual;

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var shouldMethod = context.ShouldMethod.PascalToSpaced();

        if (context.IsNegatedAssertion)
        {
            if (codePart == "null")
            {
                codePart = expected;
            }

            return
                $"""
                 {codePart}
                     {shouldMethod} but was{(context.Expected == null ? " null" : "")}
                 """;
        }

        string details;
        if (context.Expected is IEnumerable enumerable and not string)
        {
            var (count, noun) = enumerable.DescribeCountForMessage();
            details = $"""
                        had
                       {count}
                           {noun} and
                       """;
        }
        else
        {
            details = string.Empty;
        }

        string expectedString;
        if (codePart == "null")
        {
            codePart = expected;
            expectedString = " not empty";
        }
        else
        {
            expectedString = $"""

                              {expected}
                              """;
        }

        return
            $"""
             {codePart}
                 {shouldMethod} but{details} was{expectedString}
             """;
    }
}