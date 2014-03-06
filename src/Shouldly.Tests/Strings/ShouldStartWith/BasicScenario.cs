namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldStartWith("Ce");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should start with \"Ce\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldStartWith("Ch");
        }
    }
}