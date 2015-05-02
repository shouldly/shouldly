using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected string[] target = new[] { "a", "b", "c" };

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldContain("d", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should contain \"d\" but was actually [\"a\", \"b\", \"c\"]" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldContain("b");
        }
    }
}