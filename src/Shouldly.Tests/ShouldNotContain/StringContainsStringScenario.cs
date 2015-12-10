using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringScenario
    {
        protected string target = "Shouldly is legendary";

        [Fact]
        public void StringContainsStringScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    target.ShouldNotContain("legendary"),

    errorWithSource:
    @"target should not contain ""legendary"" (case insensitive comparison)
    but was
actually ""Shouldly is legendary""",

    errorWithoutSource:
    @"target should not contain ""legendary"" (case insensitive comparison)
    but was
actually ""Shouldly is legendary""");
        }

        [Fact]
        public void ShouldPass()
        {
            target.ShouldNotContain("legend-wait for it-ary");
        }
    }
}