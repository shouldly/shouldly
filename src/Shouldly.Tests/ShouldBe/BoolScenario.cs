namespace Shouldly.Tests.ShouldBe
{
    public class BoolScenario : ShouldlyTestScenario
    {
        protected override void ShouldPass()
        {
            true.ShouldBe(true);
        }

        protected override void ShouldThrowAWobbly()
        {
            const bool myValue = false;
            myValue.ShouldBe(true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myValue should be True but was False"; }
        }

        protected override void NotVersionShouldPass()
        {
            false.ShouldNotBe(true);
        }

        protected override void NotVersionShouldThrowAWobbly()
        {
            const bool myFalseValue = false;
            myFalseValue.ShouldNotBe(false);
        }

        protected override string NotVersionChuckedAWobblyErrorMessage
        {
            get { return "myFalseValue should not be False but was False"; }
        }
    }
}