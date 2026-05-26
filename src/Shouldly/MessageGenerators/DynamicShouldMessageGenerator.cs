namespace Shouldly.MessageGenerators;

class DynamicShouldMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("HaveProperty", RegexOptions.Compiled);

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Expected is object);

        var propertyName = context.Expected;
        var codePart = context.CodePart;

        // CodePart is "null" when DisableSourceInErrors is set (the value-only fallback). In that
        // case render the generic form without the receiver name.
        if (string.IsNullOrEmpty(codePart) || codePart == "null")
        {
            const string genericFormat = """Dynamic object should contain property "{0}" but does not.""";
            return string.Format(genericFormat, propertyName?.ToString()?.Trim());
        }

        const string format = """Dynamic object "{0}" should contain property "{1}" but does not.""";
        return string.Format(format, codePart!.Trim(), propertyName?.ToString()?.Trim());
    }
}
