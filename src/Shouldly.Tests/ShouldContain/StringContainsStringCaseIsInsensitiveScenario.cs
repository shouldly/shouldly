using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsStringCaseIsInsensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const string target = "Shouldly is legendary";
            target.ShouldContain("legend-wait for it-ary", Case.Insensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should contain \"legend-wait for it-ary\" (case insensitive comparison) but was actually \"Shouldly is legendary\""; }
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldContain("LEGENDARY", Case.Insensitive);
        }
    }
}