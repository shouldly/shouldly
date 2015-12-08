using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroLongScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
           0L.ShouldBeNegative("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"0L should be negative but 0 is positive

Additional Info:
    Some additional context";
            }
        }
    }
}