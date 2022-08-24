using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBePositive
{
    public class DoubleScenario
    {
        [Fact]
        [UseCulture("en-US")]
        public void DoubleScenarioShouldFail()
        {
            var @double = -3.5;
            Verify.ShouldFail(() =>
@double.ShouldBePositive("Some additional context"),

errorWithSource:
@"@double
    should be positive but
-3.5d
    is negative

Additional Info:
    Some additional context",

errorWithoutSource:
@"-3.5d
    should be positive but is negative

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            7.5.ShouldBePositive();
        }
    }
}