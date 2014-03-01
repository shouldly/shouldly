using System.Collections.Generic;

namespace Shouldly.Tests.ShouldBe.EnumerableScenarios
{
    public class DifferentCollectionTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 });
        }

        protected override void ShouldThrowAWobbly()
        {
            new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 3, 2 });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new List<int> { 1, 2, 3 } should be [1, 3, 2] but was [1, 2, 3] difference [1, *2*, *3*]"; }
        }
    }
}