using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNull
{
    public class NullScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            ((string)null).ShouldBeNull();
        }

        protected override void ShouldThrowAWobbly()
        {
            string myNullRef = "Hello World";
            myNullRef.ShouldBeNull("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"myNullRef should be null but was ""Hello World""
Additional Info:
Some additional context";
            }
        }
    }
}