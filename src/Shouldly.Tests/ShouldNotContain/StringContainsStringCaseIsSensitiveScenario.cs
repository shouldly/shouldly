using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            const string target = "Shouldly is legendary";
            target.ShouldNotContain("legendary", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "target should not contain \"legendary\" but was actually \"Shouldly is legendary\""; }
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Sensitive);
        }
    }
}