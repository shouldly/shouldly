using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroShortScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           ((short)0).ShouldBePositive("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"(short)0 should be positive but 0 is negative
Additional Info:
    Some additional context";
            }
        }
    }
}