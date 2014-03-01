namespace Shouldly.Tests.ShouldBe
{
    public class NumbersOfDifferentTypesScenario : ShouldlyTestScenario
    {
        protected override void ShouldPass()
        {
            1L.ShouldBe(1);
        }

        protected override void ShouldThrowAWobbly()
        {
            const long aLong = 2L;
            aLong.ShouldBe(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "aLong should be 1 but was 2"; }
        }

        protected override void NotVersionShouldPass()
        {
            1L.ShouldNotBe(2);
        }

        protected override void NotVersionShouldThrowAWobbly()
        {
            const long aLong = 1L;
            aLong.ShouldNotBe(1);
        }

        protected override string NotVersionChuckedAWobblyErrorMessage
        {
            get { return "aLong should not be 1 but was 1"; }
        }
    }
}