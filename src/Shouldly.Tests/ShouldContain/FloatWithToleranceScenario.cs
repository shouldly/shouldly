using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class FloatWithToleranceScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1f, 2f, 3f}.ShouldContain(1.8f, 0.1d, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[]{1f, 2f, 3f} should contain 1.8f within 0.1d but was [1f, 2f, 3f]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1f, 2f, 3f}.ShouldContain(1.91f, 0.1d);
        }
    }
}