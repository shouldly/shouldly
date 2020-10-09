using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("Should(Not)?BeEmpty");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasRelevantActual;
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var shouldMethod = context.ShouldMethod.PascalToSpaced();

            if (context.IsNegatedAssertion)
            {
                if (codePart == "null")
                {
                    codePart = expected;
                }

                return
$@"{codePart}
    {shouldMethod} but was{(context.Expected == null ? " null" : "")}";
            }

            var count = (context.Expected ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>().Count();
            string details;
            if (!(context.Expected is string) && context.Expected is IEnumerable)
            {
                details = $@" had
{count}
    item{(count == 1 ? string.Empty : "s")} and";
            }
            else
            {
                details = string.Empty;
            }
            string expectedString;
            if (codePart == "null")
            {
                codePart = expected;
                expectedString = " not empty";
            }
            else expectedString = $@"
{expected}";

            return
$@"{codePart}
    {shouldMethod} but{details} was{expectedString}";
        }
    }
}