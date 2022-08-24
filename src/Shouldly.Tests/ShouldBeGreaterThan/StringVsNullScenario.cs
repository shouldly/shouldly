using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class StringVsNullScenario
    {
        [Fact]
        public void StringVsNullScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
((string?)null).ShouldBeGreaterThan("b", "Some additional context"),

errorWithSource:
@"(string?)null
    should be greater than
""b""
    but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be greater than
""b""
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "b".ShouldBeGreaterThan(null);
        }
    }
}