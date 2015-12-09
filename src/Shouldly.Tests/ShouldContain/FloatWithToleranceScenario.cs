using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldContain
{
    public class FloatWithToleranceScenario
    {
        [Fact]
        public void FloatWithToleranceScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1f, 2f, 3f }.ShouldContain(1.8f, 0.1d, "Some additional context"),

errorWithSource:
@"new[] { 1f, 2f, 3f }
    should contain
1.8
    within
0.1
    but was
[1, 2, 3]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should contain
1.8
    within
0.1
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1f, 2f, 3f }.ShouldContain(1.91f, 0.1d);
        }
    }
}