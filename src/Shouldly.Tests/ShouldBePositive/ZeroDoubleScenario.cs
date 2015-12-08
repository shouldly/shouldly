using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroDoubleScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           0.0.ShouldBePositive("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0.0 should be positive but 0 is negative
Additional Info:
    Some additional context";
            }
        }
    }
}