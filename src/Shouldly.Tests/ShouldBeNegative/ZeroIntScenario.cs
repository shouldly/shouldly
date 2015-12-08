using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroIntScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var zeroInt = 0;
            zeroInt.ShouldBeNegative("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"zeroInt should be negative but 0 is positive
Additional Info:
    Some additional context";
            }
        }
    }
}