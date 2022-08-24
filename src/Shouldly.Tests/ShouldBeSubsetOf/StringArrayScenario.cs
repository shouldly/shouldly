using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class StringArrayScenario
    {
        [Fact]
        public void StringArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { "1", "2", "3" }.ShouldBeSubsetOf(new[] { "1", "2" }, "Some additional context"),

errorWithSource:
@"new[] { ""1"", ""2"", ""3"" }
    should be subset of
[""1"", ""2""]
    but
[""3""]
    is outside subset

Additional Info:
    Some additional context",

errorWithoutSource:
@"[""1"", ""2"", ""3""]
    should be subset of
[""1"", ""2""]
    but
[""3""]
    is outside subset

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { "1", "2", "3" }.ShouldBeSubsetOf(new[] { "1", "2", "3", "4" });
        }
    }
}