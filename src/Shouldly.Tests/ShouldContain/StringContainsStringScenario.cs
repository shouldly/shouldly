using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsStringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Shouldly is legendary".ShouldContain("legend-wait for it-dary");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Shouldly is legendary\" should contain \"legend-wait for it-dary\" but did not"; } 
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldContain("legendary");
        }
    }
}