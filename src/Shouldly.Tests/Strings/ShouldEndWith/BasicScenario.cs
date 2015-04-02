using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldEndWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldEndWith("ze", () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"Cheese\" should end with \"ze\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldEndWith("se");
        }
    }
}