using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected int[] target = new[] { 1, 2, 3, 4, 5 };

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain(3, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should not contain 3 but was actually [1, 2, 3, 4, 5]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain(7);
        }
    }
}