using System.Collections.Generic;

namespace Shouldly.Tests.ShouldBe.EnumerableScenarios
{
    public class IgnoringOrderScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
        }

        protected override void ShouldThrowAWobbly()
        {
            new List<int> { 1, 4, 2 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new List<int> { 1, 4, 2 } should be [1, 2, 3] but was [1, 4, 2] difference [1, *4*, *2*]"; }
        }
    }
}