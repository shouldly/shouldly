namespace Shouldly.Tests.ShouldBe
{
    public class BoolScenario : ShouldlyShouldTestScenario
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
    }
}