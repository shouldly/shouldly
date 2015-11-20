using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests
{
    public class ShouldNotBeInRangeTests : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.5m.ShouldNotBeInRange(1.4m, 1.6m, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            { 
                return @"1.5m should not be in range { from = 1.4, to = 1.6 } but was 1.5
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            1.5m.ShouldNotBeInRange(1.6m, 1.7m);
        }
    }
}
