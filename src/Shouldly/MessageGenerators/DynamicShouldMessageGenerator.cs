#if net40
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class DynamicShouldMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("HaveProperty", RegexOptions.Compiled);
        private static readonly Regex DynamicObjectNameExtractor = new Regex(@"DynamicShould.HaveProperty\((?<dynamicObjectName>.*),(?<propertyName>.*)\)", RegexOptions.Compiled);
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format =  @"
    Dynamic object
        ""{0}""
    should contain property
        {1}
    but does not.";

            var testFileName = context.OriginatingFrame.GetFileName();
            var assertionLineNumber = context.OriginatingFrame.GetFileLineNumber();

            var codeLine = string.Join("", File.ReadAllLines(testFileName).ToArray().Skip(assertionLineNumber - 1).Select(s => s.Trim()));
            var dynamicObjectName = DynamicObjectNameExtractor.Match(codeLine).Groups["dynamicObjectName"];
            var propertyName = DynamicObjectNameExtractor.Match(codeLine).Groups["propertyName"];

            return String.Format(format, dynamicObjectName, propertyName);
        }
    }
}
#endif