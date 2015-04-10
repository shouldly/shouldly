using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenarioCaseSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotStartWith("Ch", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should not start with \"Ch\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("CH", Case.Sensitive);
        }
    }
}