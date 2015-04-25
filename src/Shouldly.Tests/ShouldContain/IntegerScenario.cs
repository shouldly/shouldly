using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected int[] target = new[] { 1, 2, 3, 4, 5 };

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldContain(6, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should contain 6 but was actually [1, 2, 3, 4, 5]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldContain(3);
        }
    }
}