using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNull
{
    public class NotNullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            "Hello World".ShouldNotBeNull();
        }

        protected override void ShouldThrowAWobbly()
        {
            string myNullRef = null;
            myNullRef.ShouldNotBeNull("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"myNullRef should not be null but was null
Additional Info:
Some additional context";
            }
        }
    }
}