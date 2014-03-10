using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeInRange
{
    public class DecimalScenaro : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.5m.ShouldBeInRange(1.6m, 1.7m);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1.5m should be in range { from = 1.6, to = 1.7 } but was 1.5"; }
        }

        protected override void ShouldPass()
        {
            1.5m.ShouldBeInRange(1.4m, 1.6m);
        }
    }
}