using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsStringCaseIsSensitiveScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Shouldly is LEGENDARY".ShouldContain("legendary", Case.Sensitive);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Shouldly is LEGENDARY\" should contain \"legendary\" but does not"; }
        }

        protected override void ShouldPass()
        {
            "Shouldly is LEGENDARY".ShouldContain("LEGENDARY", Case.Sensitive);
        }
    }
}