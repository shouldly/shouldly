using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class DifferentOrderWithMissingItemFromActualScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new List<int> {1, 3}.ShouldBe(new[] {1, 2, 3}, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new List<int> { 1, 3 } should be [1, 2, 3] but was [1, 3] difference [1, *3*, *]"; }
        }
    }
}