using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const string target = "Shouldly is legendary";
            target.ShouldNotContain("LEGENDARY", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should not contain \"LEGENDARY\" (case insensitive comparison) but was actually \"Shouldly is legendary\""; } 
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGEND-wait for it-ary", Case.Insensitive);
        }
    }
}