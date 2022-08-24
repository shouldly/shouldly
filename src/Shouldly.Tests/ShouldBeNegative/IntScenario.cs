using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class IntScenario
    {
    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = 3;
        Verify.ShouldFail(() =>
@int.ShouldBeNegative("Some additional context"),

errorWithSource:
@"@int
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
        (-7).ShouldBeNegative();
    }
}
}