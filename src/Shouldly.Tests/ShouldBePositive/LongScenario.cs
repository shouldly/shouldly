using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class LongScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            (-3L).ShouldBePositive("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return @"-3L should be positive but -3 is negative
Additional Info:
    Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            7L.ShouldBePositive();
        }
    }
}