using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroFloatScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            0f.ShouldBeNegative("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0f should be negative but 0 is positive
Additional Info:
    Some additional context";
            }
        }
    }
}