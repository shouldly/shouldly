using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldEndWith
{
    public class BasicScenarioCaseSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldEndWith("Se", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should end with \"Se\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldEndWith("se", Case.Sensitive);
        }
    }
}