﻿using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeSubsetOfMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBeSubsetOf", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"{0} should be subset of {1} but {2} do not";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            return string.Format(format, codePart, expectedValue, context.Actual.ToStringAwesomely());
        }
    }
}