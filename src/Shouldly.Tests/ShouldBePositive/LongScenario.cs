using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class LongScenario
    {
        [Fact]
        public void LongScenarioShouldFail()
        {
            var val = -3L;
            Verify.ShouldFail(() =>
val.ShouldBePositive("Some additional context"),

errorWithSource:
@"val
    should be positive but
-3L
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"-3L
    should be positive but is negative

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            7L.ShouldBePositive();
        }
    }
}