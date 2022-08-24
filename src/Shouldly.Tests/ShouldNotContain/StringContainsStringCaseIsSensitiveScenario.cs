using Shouldly.Tests.Strings;

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
@"target
    should not contain
""legendary""
    but was actually
""Shouldly is legendary""",

errorWithoutSource:
@"""Shouldly is legendary""
    should not contain
""legendary""
    but did");
        }

        [Fact]
        public void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Sensitive);
        }
    }
}