using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsStringScenario
    {
        private const string Target = "Shouldly is legendary";

        [Fact]
        public void StringContainsStringScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
Target.ShouldNotContain("legendary"),

errorWithSource:
@"Target
    should not contain (case insensitive comparison)
""legendary""
    but was actually
""Shouldly is legendary""",

errorWithoutSource:
@"""Shouldly is legendary""
    should not contain (case insensitive comparison)
""legendary""
    but did");
        }

        [Fact]
        public void ShouldPass()
        {
            Target.ShouldNotContain("legend-wait for it-ary");
        }
    }
}