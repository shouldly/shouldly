using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class DecimalArrayScenario
    {
        [Fact]
        public void DecimalArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1m, 2m, 5m }.ShouldBeSubsetOf(new[] { 2m, 3m, 4m }, "Some additional context"),

errorWithSource:
@"new[] { 1m, 2m, 5m }
    should be subset of
[2, 3, 4]
    but
[1, 5]
    are outside subset

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 5]
    should be subset of
[2, 3, 4]
    but
[1, 5]
    are outside subset

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1m }.ShouldBeSubsetOf(new[] { 1m, 2m, 3m });
        }
    }
}