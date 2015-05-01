using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsCharScenario : ShouldlyShouldTestScenario
    {
        protected string target = "Foo";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain('F', "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should not contain F but was actually \"Foo\"" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain('B');
        }
    }
}