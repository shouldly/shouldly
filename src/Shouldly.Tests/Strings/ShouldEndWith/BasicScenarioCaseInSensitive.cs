using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldEndWith
{
    public class BasicScenarioCaseInsensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldEndWith("ze", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should end with \"ze\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldEndWith("Se", Case.Insensitive);
        }
    }
}