using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeUnique
{
    public class ObjectArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new object[] { 1, 2, 3, 4, 7 } should not be unique [1, 2, 3, 4, 7] but does";
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

