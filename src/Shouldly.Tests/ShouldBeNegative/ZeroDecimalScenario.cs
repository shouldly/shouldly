using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroDecimalScenario
    {
        [Fact]
        public void ZeroDecimalScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
0m.ShouldBeNegative("Some additional context"),

errorWithSource:
@"0m should be negative but 0 is positive
Additional Info:
    Some additional context",

errorWithoutSource:
@"0m should be negative but 0 is positive
Additional Info:
    Some additional context");
        }
    }
}