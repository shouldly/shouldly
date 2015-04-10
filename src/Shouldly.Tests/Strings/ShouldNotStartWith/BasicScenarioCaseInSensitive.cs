using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenarioCaseInSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotStartWith("cH", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should not start with \"cH\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce", Case.Insensitive);
        }
    }
}