namespace Shouldly.MessageGenerators;

class DynamicShouldMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("HaveProperty", RegexOptions.Compiled);
    private static readonly Regex DynamicObjectNameExtractor = new(@"DynamicShould.HaveProperty\((?<dynamicObjectName>.*?),(?<propertyName>.*?)[\),]", RegexOptions.Compiled);

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        Validator.IsMatch(context.ShouldMethod);

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Expected is object);

        var propertyName = context.Expected;

        // Prefer CodePart (CAE-supplied or stack-walked expression) over re-reading the source
        // file. CodePart is "null" when DisableSourceInErrors is set or the call was dynamic-dispatched
        // such that neither CAE fired nor the stack-walker recovered the receiver — fall back in that case.
        var codePart = context.CodePart;
        var hasExpression = !string.IsNullOrEmpty(codePart) && codePart != "null";
        if (hasExpression)
        {
            const string format = """Dynamic object "{0}" should contain property "{1}" but does not.""";
            return string.Format(format, codePart!.Trim(), propertyName?.ToString()?.Trim());
        }

        // Legacy fallback: parse the source line via regex when FileName/LineNumber are available
        // (covers older callers and any edge case where the receiver expression wasn't captured).
        if (context.FileName != null && context.LineNumber != null)
        {
            var codeLine = string.Join("", File.ReadAllLines(context.FileName).Skip(context.LineNumber.Value - 1).Select(s => s.Trim()));
            var match = DynamicObjectNameExtractor.Match(codeLine);
            if (match.Success)
            {
                const string format = """Dynamic object "{0}" should contain property "{1}" but does not.""";
                return string.Format(format, match.Groups["dynamicObjectName"].ToString().Trim(), propertyName?.ToString()?.Trim());
            }
        }

        const string genericFormat = """Dynamic object should contain property "{0}" but does not.""";
        return string.Format(genericFormat, propertyName?.ToString()?.Trim());
    }
}