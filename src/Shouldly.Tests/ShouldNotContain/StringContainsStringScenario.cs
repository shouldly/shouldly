using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Shouldly is legendary".ShouldNotContain("legendary");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Shouldly is legendary\" should not contain \"legendary\" but was \"Shouldly is legendary\""; } 
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("legend-wait for it-ary");
        }
    }
}