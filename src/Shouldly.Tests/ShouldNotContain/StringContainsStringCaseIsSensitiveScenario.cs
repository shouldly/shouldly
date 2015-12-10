using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsSensitiveScenario
    {
        [Fact]
        public void StringContainsStringCaseIsSensitiveScenarioShouldFail()
        {
            const string target = "Shouldly is legendary";
            Verify.ShouldFail(() =>
                target.ShouldNotContain("legendary", Case.Sensitive),

    errorWithSource:
    @"target should not contain ""legendary""
    but was
actually ""Shouldly is legendary""",

    errorWithoutSource:
    @"target should not contain ""legendary""
    but was
actually ""Shouldly is legendary""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Sensitive);
        }
    }
}