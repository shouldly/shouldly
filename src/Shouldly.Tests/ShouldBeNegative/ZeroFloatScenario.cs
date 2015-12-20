using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroFloatScenario
    {
        [Fact]
        public void ZeroFloatScenarioShouldFail()
        {
            var val = 0f;
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
@"0
    should be negative but
0
    is positive

Additional Info:
    Some additional context");
        }
    }
}