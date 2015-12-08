using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroFloatScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           0f.ShouldBePositive("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0f should be positive but 0 is negative
Additional Info:
    Some additional context";
            }
        }
    }
}