using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsStringScenario : ShouldlyShouldTestScenario
    {
        protected string target = "Shouldly is legendary";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldContain("legend-wait for it-ary");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should contain \"legend-wait for it-ary\" but was actually \"Shouldly is legendary\""; } 
        }

        protected override void ShouldPass()
        {
            target.ShouldContain("legendary");
        }
    }
}