using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class EnumerableOfStringIsSensitiveScenario
    {
        [Fact]
        public void EnumerableOfStringIsSensitiveScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { "foo" }.ShouldBe(new[] { "FoO" }, Case.Sensitive, "Some additional context"),

errorWithSource:
@"new[] { ""foo"" }
    should be
[""FoO""]
    but was (case sensitive comparison)
[""foo""]
    difference
[*""foo""*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[""foo""]
    should be
[""FoO""]
    but was not (case sensitive comparison)
    difference
[*""foo""*]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { "foo" }.ShouldBe(new[] { "foo" }, Case.Sensitive);
        }
    }
}