using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class DecimalScenario
    {
        [Fact]
        public void DecimalScenarioShouldFail()
        {
            var @decimal = 3.5m;
            Verify.ShouldFail(() =>
    @decimal.ShouldBeNegative("Some additional context"),

errorWithSource:
@"@decimal
    should be negative but
3.5m
    is positive

Additional Info:
    Some additional context",

errorWithoutSource:
@"3.5m
    should be negative but is positive

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            (-7.5m).ShouldBeNegative();
        }
    }
}