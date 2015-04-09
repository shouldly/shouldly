using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseInSensitive : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotEndWith("SE", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should not end with \"SE\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze", Case.Insensitive);
        }
    }
}