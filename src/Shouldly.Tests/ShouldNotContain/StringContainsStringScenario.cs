using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringScenario : ShouldlyShouldTestScenario
    {
        protected string target = "Shouldly is legendary";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldNotContain("legendary");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should not contain \"legendary\" (case insensitive comparison) but was actually \"Shouldly is legendary\""; } 
        }

        protected override void ShouldPass()
        {
            target.ShouldNotContain("legend-wait for it-ary");
        }
    }
}