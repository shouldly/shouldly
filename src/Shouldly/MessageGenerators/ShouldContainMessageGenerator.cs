using System;
using System.Linq;
using System.Linq.Expressions;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators
{
    internal class ShouldContainMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return context.ShouldMethod.StartsWith("Should")
                   && context.ShouldMethod.Contains("Contain")
                   && context.Expected.GetType().BaseType != typeof(BinaryExpression);
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            const string format = @"
        {0}
    should contain
        {1}
    but did not";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            var message = string.Format(format, codePart, expectedValue);

            return message;
        }
    }
}