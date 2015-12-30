using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class IntScenario
    {

    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
one.ShouldBeGreaterThanOrEqualTo(7, "Some additional context"),

errorWithSource:
@"one
    should be greater than or equal to
7
    but was
1

Additional Info:
    Some additional context",

errorWithoutSource:
@"1
    should be greater than or equal to
7
    but was not

Additional Info:
    Some additional context");
    }

        [Fact]
    public void ShouldPass()
    {
        1.ShouldBeGreaterThanOrEqualTo(1);
    }
}
}