using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldNotThrowMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("NotThrow", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            // When the static method is used instead of the extension method,
            // the default message generator will extract "Should" as CodePart.
            if (context.DeterminedOriginatingFrame && context.CodePart == "Should")
            {
                var codeLines = string.Join("\n", File.ReadAllLines(context.FileName).Skip(context.LineNumber).ToArray());

                var indexOf = 
                    codeLines.IndexOf(context.ShouldMethod) +
                    context.ShouldMethod.Length +
                    1;
                var indexOfComma = codeLines.IndexOf(",");
                if (indexOf > 0 && indexOfComma > 0 && indexOfComma > indexOf)
                    codePart = codeLines.Substring(indexOf, indexOfComma - indexOf).Trim();
            }

            var expectedValue = context.Expected.ToStringAwesomely();

            const string format = @"{0} should not throw but threw {1}";
            string errorMessage = string.Format(format, codePart, expectedValue);

            var notThrowAssertionContext = context as ShouldNotThrowAssertionContext;
            errorMessage += (notThrowAssertionContext != null) ? string.Format(" with message \"{0}\"", notThrowAssertionContext.ExceptionMessage) : string.Empty;

            return errorMessage;
        }
    }
}