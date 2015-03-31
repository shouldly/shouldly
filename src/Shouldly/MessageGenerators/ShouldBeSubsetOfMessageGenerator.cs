using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeSubsetOfMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldBeSubsetOf", RegexOptions.Compiled);

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            const string format = @"{0} should be subset of {1} but {2} {3} outside subset";

            var codePart = context.CodePart;
            var expectedValue = context.Expected.ToStringAwesomely();

            var count = (context.Actual ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>().Count();

            return string.Format(format, codePart, expectedValue, context.Actual.ToStringAwesomely(), count > 1 ? "are" : "is");
        }
    }
}