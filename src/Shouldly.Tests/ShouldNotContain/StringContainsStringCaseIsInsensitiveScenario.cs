using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringCaseIsInsensitiveScenario
    {

        [Fact]
        public void StringContainsStringCaseIsInsensitiveScenarioShouldFail()
        {
            const string target = "Shouldly is legendary";
            Verify.ShouldFail(() =>
                target.ShouldNotContain("LEGENDARY", Case.Insensitive),

    errorWithSource:
    @"target should not contain ""LEGENDARY"" (case insensitive comparison)
    but was
actually ""Shouldly is legendary""",

    errorWithoutSource:
    @"target should not contain ""LEGENDARY"" (case insensitive comparison)
    but was
actually ""Shouldly is legendary""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Shouldly is legendary".ShouldNotContain("LEGEND-wait for it-ary", Case.Insensitive);
        }
    }
}