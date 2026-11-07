namespace Shouldly.MessageGenerators;

class ShouldBeNullMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("Should(Not)?BeNull");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        // Built-in ShouldBeNull/ShouldNotBeNull pass the value under test as Expected. Custom
        // assertions using AssertAwesomely pass it as Actual, so prefer that when it's relevant.
        var expected = (context.HasRelevantActual ? context.Actual : context.Expected).ToStringAwesomely();
        var codePart = context.CodePart == "null" ? expected : context.CodePart;
        var expectedValue = context.IsNegatedAssertion || expected == codePart ? string.Empty : $"""

             {expected}
             """;

        return $"""
                {codePart}
                    {context.ShouldMethod.PascalToSpaced()} but was{expectedValue}
                """;
    }
}