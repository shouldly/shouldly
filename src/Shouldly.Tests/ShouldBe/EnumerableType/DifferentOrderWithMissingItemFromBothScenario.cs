using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class DifferentOrderWithMissingItemFromBothScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var first = new List<int> { 1, 3, 2 };
            var second = new[] { 1, 3, 4 };
            first.ShouldBe(second, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "first should be [1, 3, 4] (ignoring order) but first is missing [4] and [1, 3, 4] is missing [2]"; }
        }
    }
}