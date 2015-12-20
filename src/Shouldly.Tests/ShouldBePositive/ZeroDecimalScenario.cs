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

// TODO is zero negative?
errorWithSource:
@"val
    should be positive but
0
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"0
    should be positive but is negative

Additional Info:
    Some additional context");
        }
    }
}