using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeGreaterThanOrEqualTo(7, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "1 should be greater than or equal to 7 but was 1" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}