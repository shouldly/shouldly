using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class FloatScenario
    {
        [Fact]
        public void FloatScenarioShouldFail()
        {
            var @float = 3f;
            Verify.ShouldFail(() =>
@float.ShouldBeNegative("Some additional context"),

errorWithSource:
@"@float
    should be negative but
3
    is positive

Additional Info:
    Some additional context",

errorWithoutSource:
@"3
    should be negative but is positive

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            (-7f).ShouldBeNegative();
        }
    }
}