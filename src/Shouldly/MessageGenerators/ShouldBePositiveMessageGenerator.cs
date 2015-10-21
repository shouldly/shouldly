using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBePositiveMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBePositive", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"
    {0} was {1} and
            {2}
    but wasn't";

            var codePart = context.CodePart;
            var actual = context.Actual.ToStringAwesomely();

            return String.Format(format, codePart, actual, context.ShouldMethod.PascalToSpaced());
        }
    }
}