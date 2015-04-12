using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotEndWith("se", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "\"Cheese\" should not end with \"se\" but was \"Cheese\" " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze");
        }
    }
}