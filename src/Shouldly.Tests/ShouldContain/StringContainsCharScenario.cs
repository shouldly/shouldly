using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsCharScenario : ShouldlyShouldTestScenario
    {
        protected string target = "Foo";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldContain('B', "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should contain B but was actually \"Foo\"" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldContain('F');
        }
    }
}