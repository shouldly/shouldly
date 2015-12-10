using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringArrayScenario
    {
        protected string[] target = new[] { "a", "b", "c" };

        [Fact]
        public void StringArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    target.ShouldNotContain("c", "Some additional context"),

    errorWithSource:
    @"target should not contain ""c""
    but was
actually [""a"", ""b"", ""c""]
Additional Info:
    Some additional context",

    errorWithoutSource:
    @"target should not contain ""c""
    but was
actually [""a"", ""b"", ""c""]
Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            target.ShouldNotContain("d");
        }
    }
}