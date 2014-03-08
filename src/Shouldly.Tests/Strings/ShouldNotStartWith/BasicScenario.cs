namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotStartWith("Ch");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should not start with \"Ch\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce");
        }
    }
}