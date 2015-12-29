using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class ShortScenario
    {
        [Fact]
        public void ShortScenarioShouldFail()
        {
            var @short = ((short)-3);
            Verify.ShouldFail(() =>
@short.ShouldBePositive("Some additional context"),

errorWithSource:
@"@short
    should be positive but
-3
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"-3
    should be positive but is negative

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            ((short)7).ShouldBePositive();
        }
    }
}