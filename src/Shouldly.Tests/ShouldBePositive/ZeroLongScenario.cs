using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroLongScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           0L.ShouldBePositive("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0L should be positive but 0 is negative
Additional Info:
    Some additional context";
            }
        }
    }
}