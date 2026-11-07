namespace Shouldly.MessageGenerators;

class ShouldBeNullMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("Should(Not)?BeNull");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        // Built-in ShouldBeNull/ShouldNotBeNull pass the value under test as Expected; custom
        // assertions using AssertAwesomely pass it as Actual.
        var value = (context.HasRelevantActual ? context.Actual : context.Expected).ToStringAwesomely();
        var codePart = context.CodePart == "null" ? value : context.CodePart;
        var valuePart = context.IsNegatedAssertion || value == codePart ? string.Empty : $"""

             {value}
             """;

        return $"""
                {codePart}
                    {context.ShouldMethod.PascalToSpaced()} but was{valuePart}
                """;
    }
}