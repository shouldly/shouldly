using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators;

internal class ShouldBeUniqueMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldBeUnique");

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return Validator.IsMatch(context.ShouldMethod) && context.HasRelevantActual;
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var actual = context.Actual.ToStringAwesomely();

        if (codePart == actual)
        {
            codePart = context.Expected.ToStringAwesomely();
        }

        return
            $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()} but
{actual}
    was duplicated";
    }
}