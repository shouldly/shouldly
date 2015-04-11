using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {"a", "b", "c"}.ShouldNotContain("c", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[]{\"a\", \"b\", \"c\"} should not contain \"c\" but does" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {"a", "b", "c"}.ShouldNotContain("d", () => "Some additional context");
        }
    }
}