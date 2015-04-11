using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1, 2, 3, 4, 5 }.ShouldContain(6, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1, 2, 3, 4, 5 } should contain 6 but does not" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] { 1, 2, 3, 4, 5 }.ShouldContain(3, () => "Some additional context");
        }
    }
}