using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroDoubleScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            0.0.ShouldBeNegative("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0.0 should be negative but 0 is positive
Additional Info:
    Some additional context";
            }
        }
    }
}