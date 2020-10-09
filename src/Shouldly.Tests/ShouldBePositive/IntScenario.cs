using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBePositive
{
    public class IntScenario
    {

    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = -3;
        Verify.ShouldFail(() =>
@int.ShouldBePositive("Some additional context"),

errorWithSource:
@"@int
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
        7.ShouldBePositive();
    }
}
}