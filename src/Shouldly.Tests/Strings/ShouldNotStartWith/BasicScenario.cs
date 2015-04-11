using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotStartWith("Ch", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"Cheese\" should not start with \"Ch\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce");
        }
    }
}