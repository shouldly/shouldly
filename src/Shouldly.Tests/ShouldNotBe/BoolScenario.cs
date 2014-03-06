namespace Shouldly.Tests.ShouldNotBe
{
    public class BoolScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            false.ShouldNotBe(true);
        }

        protected override void ShouldThrowAWobbly()
        {
            const bool myFalseValue = false;
            myFalseValue.ShouldNotBe(false);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myFalseValue should not be False but was False"; }
        }
    }
}