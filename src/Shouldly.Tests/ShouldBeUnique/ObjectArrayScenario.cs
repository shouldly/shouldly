using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeUnique
{
    public class ObjectArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new object[] { 1, 2, 3, 4, 2 } should be unique [1, 2, 3, 4, 2] but does not";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new object[] { 1, 2, 3, 4, 2 }.ShouldBeUnique();
        }

        protected override void ShouldPass()
        {
            new object[] { 1, 2, 3, 4, 7 }.ShouldBeUnique();
        }
    }
}

