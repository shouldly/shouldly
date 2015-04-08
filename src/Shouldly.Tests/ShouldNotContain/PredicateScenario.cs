using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1, 2, 3}.ShouldNotContain(i => i < 4, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1, 2, 3 } should not contain an element satisfying the condition (i < 4) but does" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1, 2, 3}.ShouldNotContain(i => i > 3, () => "Some additional context");
        }
    }
}