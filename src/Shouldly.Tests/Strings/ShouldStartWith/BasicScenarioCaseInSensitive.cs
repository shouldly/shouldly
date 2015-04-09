using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenarioCaseInSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldStartWith("Ce", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should start with \"Ce\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldStartWith("CH", Case.Insensitive);
        }
    }
}