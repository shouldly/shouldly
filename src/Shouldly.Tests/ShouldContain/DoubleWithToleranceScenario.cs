using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class DoubleWithToleranceScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1d, 2d, 3d}.ShouldContain(1.8, 0.1d, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[]{1d, 2d, 3d} should contain 1.8d within 0.1d but was [1d, 2d, 3d]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1d, 2d, 3d}.ShouldContain(1.91d, 0.1d);
        }
    }
}