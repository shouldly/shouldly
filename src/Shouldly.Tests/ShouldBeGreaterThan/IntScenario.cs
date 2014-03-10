using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeGreaterThan(7);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should be greater than 7 but was 1"; }
        }

        protected override void ShouldPass()
        {
            7.ShouldBeGreaterThan(1);
        }
    }
}