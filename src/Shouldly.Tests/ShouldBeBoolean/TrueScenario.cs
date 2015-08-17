using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeBoolean
{
    public class TrueScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            true.ShouldBeTrue();
        }

        protected override void ShouldThrowAWobbly()
        {
            const bool myValue = false;
            myValue.ShouldBeTrue("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"myValue should be True but was False
Additional Info:
Some additional context";
            }
        }
    }
}