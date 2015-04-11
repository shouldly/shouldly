using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeUnique
{
    public class ObjectArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new object[] { 1, 2, 3, 4, 2 } should be unique but [2] was duplicated" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new object[] {1, 2, 3, 4, 2}.ShouldBeUnique("Some additional context");
        }

        protected override void ShouldPass()
        {
            new object[] {1, 2, 3, 4, 7}.ShouldBeUnique(() => "Some additional context");
        }
    }
}

