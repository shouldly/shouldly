namespace Shouldly.Tests.ShouldBe
{
    public class IntegerScenario : ShouldlyTestScenario
    {
        protected override void ShouldPass()
        {
            1.ShouldBe(1);
        }

        protected override void ShouldThrowAWobbly()
        {
            const int two = 2;
            two.ShouldBe(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "two should be 1 but was 2"; }
        }

        protected override void NotVersionShouldPass()
        {
            1.ShouldNotBe(2);
        }

        protected override void NotVersionShouldThrowAWobbly()
        {
            const int one = 1;
            one.ShouldNotBe(1);
        }

        protected override string NotVersionChuckedAWobblyErrorMessage
        {
            get { return "one should not be 1 but was 1"; }
        }
    }
}