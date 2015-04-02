using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldStartWith("Ce", () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"Cheese\" should start with \"Ce\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldStartWith("Ch");
        }
    }
}