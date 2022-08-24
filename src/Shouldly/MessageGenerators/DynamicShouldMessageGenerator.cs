using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators;

internal class DynamicShouldMessageGenerator : ShouldlyMessageGenerator
{
    private static readonly Regex Validator = new("HaveProperty", RegexOptions.Compiled);
    private static readonly Regex DynamicObjectNameExtractor = new(@"DynamicShould.HaveProperty\((?<dynamicObjectName>.*?),(?<propertyName>.*?)[\),]", RegexOptions.Compiled);

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return Validator.IsMatch(context.ShouldMethod);
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Expected is object);

        var propertyName = context.Expected;

        var testFileName = context.FileName;
        var assertionLineNumber = context.LineNumber;

        if (testFileName != null && assertionLineNumber != null)
        {
            var codeLine = string.Join("", File.ReadAllLines(testFileName).ToArray().Skip(assertionLineNumber.Value - 1).Select(s => s.Trim()));
            var dynamicObjectName = DynamicObjectNameExtractor.Match(codeLine).Groups["dynamicObjectName"];

            const string format = @"Dynamic object ""{0}"" should contain property ""{1}"" but does not.";
            return string.Format(format, dynamicObjectName.ToString().Trim(), propertyName?.ToString()?.Trim());
        }
        else
        {
            const string format = @"Dynamic object should contain property ""{0}"" but does not.";
            return string.Format(format, propertyName?.ToString()?.Trim());
        }
    }
}