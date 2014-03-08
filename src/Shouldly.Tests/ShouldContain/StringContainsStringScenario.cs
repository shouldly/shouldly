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
            get { return "\"Shouldly is legendary\" should contain \"legend-wait for it-dary\" but was \"Shouldly is legendary\""; } 
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldContain("legendary");
        }
    }
}