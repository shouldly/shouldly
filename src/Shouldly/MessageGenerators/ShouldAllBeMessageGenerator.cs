using ExpressionToString;

namespace Shouldly.MessageGenerators;

class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("ShouldAllBe");

    public override bool CanProcess(IShouldlyAssertionContext context) => Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var expected = context.Expected.ToStringAwesomely();
        var expression = ExpressionStringBuilder.ToString(context.Filter);
        return $@"{codePart}
    should satisfy the condition
{expression}
    but
{expected}
    do not";
    }
}