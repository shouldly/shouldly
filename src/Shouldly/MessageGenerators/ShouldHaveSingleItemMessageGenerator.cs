using System;
using Shouldly.DifferenceHighlighting;
using System.Collections;
using System.Linq;

namespace Shouldly.MessageGenerators
{
    internal class ShouldHaveSingleItemMessageGenerator : ShouldlyMessageGenerator
    {
        const string ShouldBeAssertion = "ShouldHaveSingleItem";

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod.Equals(ShouldBeAssertion, StringComparison.OrdinalIgnoreCase);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var count = (context.Expected ?? Enumerable.Empty<object>()).As<IEnumerable>().Cast<object>().Count();
            if (codePart != "null")
            {
                return
    $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()} but had
{count}
    items and was
{expected}";
            }
            else
            {
                return
$@"{expected}
    {context.ShouldMethod.PascalToSpaced()} but had
{count}
    items";
            }
        }
    }
}
