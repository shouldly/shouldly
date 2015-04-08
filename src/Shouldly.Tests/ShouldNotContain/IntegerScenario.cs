using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1, 2, 3, 4, 5}.ShouldNotContain(3, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1, 2, 3, 4, 5 } should not contain 3 but does" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1, 2, 3, 4, 5}.ShouldNotContain(7, () => "Some additional context");
        }
    }
}