using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class IntegerWithNegativeValuesScenario : ShouldlyShouldTestScenario
    {
        protected int[] target = new int[] { 2, 3, 4, 5, 4, 123665, 11234, -13562377 };

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain(3, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should not contain 3 " +
                       "but was actually" +
                       "[ 2, 3, 4, 5, 4, 123665, 11234, -13562377 ]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain(-300);
        }
    }
}