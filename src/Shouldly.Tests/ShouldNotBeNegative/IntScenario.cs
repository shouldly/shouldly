namespace Shouldly.Tests.ShouldNotBeNegative;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = -3;
        Verify.ShouldFail(() =>
                @int.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"@int
    should not be negative but
-3
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7.ShouldNotBeNegative();
    }
}