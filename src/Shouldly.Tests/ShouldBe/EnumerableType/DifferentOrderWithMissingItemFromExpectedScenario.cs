using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class DifferentOrderWithMissingItemFromExpectedScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 3 }, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new List<int> { 1, 3, 2 } should be [1, 3] (ignoring order) but [1, 3] is missing [2]"; }
        }
    }
}