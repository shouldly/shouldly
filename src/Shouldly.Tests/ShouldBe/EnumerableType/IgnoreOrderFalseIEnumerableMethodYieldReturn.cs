using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseIEnumerableMethodYieldReturn : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEnumerable().ShouldBe(new[] {1}, false);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEnumerable().ShouldBe(new[] { 1, 2 }, false, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"GetEnumerable() should be [1, 2] but was [1] difference [1, *]
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
