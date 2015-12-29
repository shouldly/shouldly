using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ZeroLongScenario
    {
        [Fact]
        public void ZeroLongScenarioShouldFail()
        {
            var val = 0L;
            Verify.ShouldFail(() =>
val.ShouldBePositive("Some additional context"),

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