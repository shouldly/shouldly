using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ShortScenario
    {
        [Fact]
        public void ShortScenarioShouldFail()
        {
            var @short = (short)3;
            Verify.ShouldFail(() =>
@short.ShouldBeNegative("Some additional context"),

errorWithSource:
@"@short
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
            ((short)-7).ShouldBeNegative();
        }
    }
}