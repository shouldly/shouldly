using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            1.ShouldBe(1);
        }

        protected override void ShouldThrowAWobbly()
        {
            const int two = 2;
            two.ShouldBe(1, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"two should be 1 but was 2
Additional Info:
Some additional context";
            }
        }
    }
}