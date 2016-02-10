using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringArrayScenario
    {
        readonly string[] _target = new[] { "a", "b", "c" };

        [Fact]
        public void StringArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldNotContain("c", "Some additional context"),

errorWithSource:
@"_target
    should not contain
""c""
    but was actually
[""a"", ""b"", ""c""]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[""a"", ""b"", ""c""]
    should not contain
""c""
    but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _target.ShouldNotContain("d");
        }
    }
}