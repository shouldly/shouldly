using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroDoubleScenario
    {
        [Fact]
        public void ZeroDoubleScenarioShouldFail()
        {
            var val = 0.0;
            Verify.ShouldFail(() =>
val.ShouldBeNegative("Some additional context"),

errorWithSource:
@"val
    should be negative but
0
    is positive

Additional Info:
    Some additional context",

errorWithoutSource:
@"0.0
    should be negative but
0
    is positive

Additional Info:
    Some additional context");
        }
    }
}