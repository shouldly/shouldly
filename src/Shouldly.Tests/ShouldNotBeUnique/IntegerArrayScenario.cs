using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeUnique
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new [] { 1, 2, 3 } should not be unique but was";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new [] { 1, 2, 3 }.ShouldNotBeUnique();
        }

        protected override void ShouldPass()
        {
            new [] { 1, 2, 2 }.ShouldNotBeUnique();
        }
    }
}

