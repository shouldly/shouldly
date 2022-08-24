using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class IntScenario
    {
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
one.ShouldBeGreaterThan(7, "Some additional context"),

errorWithSource:
@"one
    should be greater than
7
    but was
1

Additional Info:
    Some additional context",

errorWithoutSource:
@"1
    should be greater than
7
    but was not

Additional Info:
    Some additional context");
    }

        [Fact]
    public void ShouldPass()
    {
        7.ShouldBeGreaterThan(1);
    }
}
}