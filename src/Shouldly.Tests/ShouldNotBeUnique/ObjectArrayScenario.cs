using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeUnique
{
    public class ObjectArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new object[] { 1, 2, 3, 4, 7 } should not be unique but was";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new object[] { 1, 2, 3, 4, 7 }.ShouldNotBeUnique();
        }

        protected override void ShouldPass()
        {
            new object[] { 1, 2, 3, 4, 2 }.ShouldNotBeUnique();
        }
    }
}

