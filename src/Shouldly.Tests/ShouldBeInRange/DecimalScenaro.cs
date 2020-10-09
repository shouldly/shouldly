using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeInRange
{
    public class DecimalScenario
    {

    [Fact]
    [UseCulture("en-US")]
    public void DecimalScenarioShouldFail()
    {
        var val = 1.5m;
        Verify.ShouldFail(() =>
val.ShouldBeInRange(1.6m, 1.7m, "Some additional context"),

errorWithSource:
@"val
    should be in range
{ from = 1.6, to = 1.7 }
    but was
1.5m

Additional Info:
    Some additional context",

errorWithoutSource:
@"1.5m
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