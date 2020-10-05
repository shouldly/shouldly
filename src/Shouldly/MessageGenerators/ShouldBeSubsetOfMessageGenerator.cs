using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeSubsetOfMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldBeSubsetOf");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var actualEnumerable = (context.Actual ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>();
            var expectedEnumerable = (context.Expected ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>();

            var missing = actualEnumerable.Except(expectedEnumerable).ToList();
            var count = missing.Count;

            return
$@"{codePart}
    should be subset of
{expected}
    but
{missing.ToStringAwesomely()}
    {(count > 1 ? "are" : "is")} outside subset";
        }
    }
}