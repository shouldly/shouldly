using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class IntScenario
    {

    [Fact]
    public void IntScenarioShouldFail()
    {
        var seven = 7;
        Verify.ShouldFail(() =>
seven.ShouldBeLessThan(1, "Some additional context"),

errorWithSource:
@"seven
    should be less than
1
    but was
7

Additional Info:
    Some additional context",

errorWithoutSource:
@"7
    should be less than
1
    but was not

Additional Info:
    Some additional context");
    }

        [Fact]
    public void ShouldPass()
    {
        1.ShouldBeLessThan(7);
    }
}
}