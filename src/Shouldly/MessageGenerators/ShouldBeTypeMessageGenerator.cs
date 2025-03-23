using Shouldly.MessageGenerators;

namespace Shouldly;

class ShouldBeTypeMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBe(Not)?(OfType|AssignableTo)");

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actualType = context.Actual?.GetType().FullName;

        var actualString = codePart == actualType || codePart == "null" ? " not" : $"""

             {actualType ?? "null"}
             """;

        return
            $"""
             {codePart}
                 {context.ShouldMethod.PascalToSpaced()}
             {context.Expected.ToStringAwesomely()}
                 but was{actualString}
             """;
    }
}