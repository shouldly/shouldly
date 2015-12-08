using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroShortScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((short)0).ShouldBeNegative("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"(short)0 should be negative but 0 is positive

Additional Info:
    Some additional context";
            }
        }
    }
}