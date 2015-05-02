using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderIEnumerableMethodYieldBreak : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEmptyEnumerable().ShouldBe(new int[0], true);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEmptyEnumerable().ShouldBe(new[] { 2, 4 }, true, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"GetEmptyEnumerable() should be [2, 4] (ignoring order) but GetEmptyEnumerable() is missing [2, 4]
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
