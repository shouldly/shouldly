using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderIEnumerableMethodYieldReturn : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEnumerable().ShouldBe(new[] {1}, true);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEnumerable().ShouldBe(new[] { 1, 2 }, true, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"GetEnumerable() should be [1, 2] (ignoring order) but GetEnumerable() is missing [2]
Additional Info:
Some additional context";
            }
        }

        private static IEnumerable<int> GetEnumerable()
        {
            yield return 1;
        }
    }
}
