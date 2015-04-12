using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseIEnumerableMethodYieldBreak : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEmptyEnumerable().ShouldBe(new int[0], false);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEmptyEnumerable().ShouldBe(new[] { 2, 4 }, false, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"GetEmptyEnumerable() should be [2, 4] but was [] difference [*, *]
Additional Info:
Some additional context";
            }
        }

        private static IEnumerable<int> GetEmptyEnumerable()
        {
            yield break;
        }
    }
}
