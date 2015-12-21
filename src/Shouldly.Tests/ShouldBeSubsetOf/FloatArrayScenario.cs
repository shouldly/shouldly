using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class FloatArrayScenario
    {
        [Fact]
        public void FloatArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1f, 2f, 5f }.ShouldBeSubsetOf(new[] { 2f, 3f, 4f }, "Some additional context"),

errorWithSource:
@"new[] { 1f, 2f, 5f }
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
            new[] { 1f }.ShouldBeSubsetOf(new[] { 1f, 2f, 3f });
        }
    }
}