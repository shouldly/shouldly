using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeInRange
{
    public class DecimalScenaro
    {

    [Fact]
    public void DecimalScenaroShouldFail()
    {
        Verify.ShouldFail(() =>
1.5m.ShouldBeInRange(1.6m, 1.7m, "Some additional context"),

errorWithSource:
@"1.5m
    should be in range
{ from = 1.6, to = 1.7 }
    but was
1.5

Additional Info:
    Some additional context",

errorWithoutSource:
@"1.5
    should be in range
{ from = 1.6, to = 1.7 }
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        1.5m.ShouldBeInRange(1.4m, 1.6m);
    }
}
}