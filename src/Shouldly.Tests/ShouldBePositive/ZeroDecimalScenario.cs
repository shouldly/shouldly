using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroDecimalScenario
    {
        [Fact]
        public void ZeroDecimalScenarioShouldFail()
        {
            var val = 0m;
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