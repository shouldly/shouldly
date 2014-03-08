namespace Shouldly.Tests.ShouldNotBe
{
    public class NumbersOfDifferentTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            1L.ShouldNotBe(2);
        }

        protected override void ShouldThrowAWobbly()
        {
            const long aLong = 1L;
            aLong.ShouldNotBe(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "aLong should not be 1 but was 1"; }
        }
    }
}