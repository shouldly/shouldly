using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Shouldly is legendary".ShouldNotContain("legendary", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Shouldly is legendary\" should not contain \"legendary\" but does"; }
        }

        protected override void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Sensitive);
        }
    }
}