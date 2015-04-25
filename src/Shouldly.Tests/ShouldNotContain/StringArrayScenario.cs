using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected string[] target = new[] { "a", "b", "c" };

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain("c", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should not contain \"c\" but was actually [\"a\", \"b\", \"c\"]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain("d");
        }
    }
}