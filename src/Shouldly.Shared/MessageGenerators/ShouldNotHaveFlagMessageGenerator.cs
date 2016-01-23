using System;
using System.Collections.Generic;
using System.Text;

namespace Shouldly.MessageGenerators
{
    internal class ShouldNotHaveFlagMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod == "ShouldNotHaveFlag";
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            var actual = context.Actual.ToStringAwesomely();
            var actualString = codePart == actual ? " had" : $@" it had
{actual}";

            return $@"{codePart}
    should not have flag
{expectedValue}
    but{actualString}";
        }
    }
}
