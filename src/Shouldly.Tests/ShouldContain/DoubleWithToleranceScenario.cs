using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldContain
{
    public class DoubleWithToleranceScenario
    {

    [Fact]
    public void DoubleWithToleranceScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] {1d, 2d, 3d}.ShouldContain(1.8, 0.1d, "Some additional context"),

errorWithSource:
@"new[] {1d, 2d, 3d}
    should contain
1.8
    within
0.1
    but was
[1, 2, 3]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should contain
1.8
    within
0.1
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] {1d, 2d, 3d}.ShouldContain(1.91d, 0.1d);
    }
}
}