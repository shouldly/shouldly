namespace Shouldly.Tests.ShouldNotBeNegative;

public class LongScenario
{
    [Fact]
    public void LongScenarioShouldFail()
    {
        var val = -3L;
        Verify.ShouldFail(() =>
                val.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"val
    should not be negative but
-3L
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3L
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7L.ShouldNotBeNegative();
    }
}