using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsStringCaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const string target = "Shouldly is LEGENDARY";
            target.ShouldContain("legendary", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should contain \"legendary\" but was actually \"Shouldly is LEGENDARY\""; }
        }

        protected override void ShouldPass()
        {
            "Shouldly is LEGENDARY".ShouldContain("LEGENDARY", Case.Sensitive);
        }
    }
}