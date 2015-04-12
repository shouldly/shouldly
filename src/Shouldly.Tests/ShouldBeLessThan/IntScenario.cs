using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            7.ShouldBeLessThan(1, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "7 should be less than 1 but was 7" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeLessThan(7);
        }
    }
}