using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotEndWith("se", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should not end with \"se\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("SE", Case.Sensitive);
        }
    }
}