using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenarioCaseSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldStartWith("cH", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should start with \"cH\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldStartWith("Ch", Case.Sensitive);
        }
    }
}