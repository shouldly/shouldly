using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class DoubleScenario
    {
        [Fact]
        public void DoubleScenarioShouldFail()
        {
            var @decimal = 3.5;
            Verify.ShouldFail(() =>
@decimal.ShouldBeNegative("Some additional context"),

errorWithSource:
@"@decimal
    should be negative but
3.5d
    is positive

Additional Info:
    Some additional context",

errorWithoutSource:
@"3.5d
    should be negative but is positive

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            (-7.5).ShouldBeNegative();
        }
    }
}