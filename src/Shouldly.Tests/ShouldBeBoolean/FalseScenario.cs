using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeBoolean
{
    public class FalseScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            false.ShouldBeFalse();
        }

        protected override void ShouldThrowAWobbly()
        {
            const bool myValue = true;
            myValue.ShouldBeFalse("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"myValue should be False but was True
Additional Info:
Some additional context";
            }
        }
    }
}