using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeUnique
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new [] { 1, 2, 2 } should be unique but [2] was duplicated" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldThrowAWobbly()
        {
            new[] {1, 2, 2}.ShouldBeUnique(() => "Some additional context");
        }

        protected override void ShouldPass()
        {
            new[] {1, 2, 3}.ShouldBeUnique(() => "Some additional context");
        }
    }
}

