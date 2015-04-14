using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class NumbersOfDifferentTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            1L.ShouldBe(1);
        }

        protected override void ShouldThrowAWobbly()
        {
            const long aLong = 2L;
            aLong.ShouldBe(1, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"aLong should be 1 but was 2
Additional Info:
Some additional context";
            }
        }
    }
}