using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: false);
        }

        protected override void ShouldThrowAWobbly()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            new List<int> { 1, 4, 2 }.ShouldBe(new[] { 1, 2, 3 }, false, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"new List<int> { 1, 4, 2 } should be [1, 2, 3] but was [1, 4, 2] difference [1, *4 *, *2 *]
Additional Info:
Some additional context";
            }
        }
    }
}