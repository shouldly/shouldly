﻿using System;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeNullOrWhiteSpaceMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeNullOrWhiteSpace", RegexOptions.Compiled);
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasRelevantActual;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"
    {0}
            {1}";

            var codePart = context.CodePart;

            var isNegatedAssertion = context.ShouldMethod.Contains("Not");
            if (isNegatedAssertion)
                return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced());

            return String.Format(format, codePart, context.ShouldMethod.PascalToSpaced());
        }
    }
}