using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroFloatScenario
    {
        [Fact]
        public void ZeroFloatScenarioShouldFail()
        {
            var val = 0f;
            Verify.ShouldFail(() =>
val.ShouldBePositive("Some additional context"),

errorWithSource:
@"val should be positive but 0 is zero
Additional Info:
    Some additional context",

errorWithoutSource:
@"-3L should be positive but -3 is zero
Additional Info:
    Some additional context");
        }
    }
}