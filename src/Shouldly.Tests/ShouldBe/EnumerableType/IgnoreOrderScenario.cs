using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
        }

        protected override void ShouldThrowAWobbly()
        {
            new List<int> { 1, 4, 2 }.ShouldBe(new[] { 1, 2, 3 }, true, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"new List<int> { 1, 4, 2 } should be [1, 2, 3] (ignoring order) 
but new List<int> { 1, 4, 2 } is missing [3] and [1, 2, 3] is missing [4]
Additional Info:
Some additional context";
            }
        }
    }
}