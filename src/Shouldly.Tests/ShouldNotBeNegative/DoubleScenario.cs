namespace Shouldly.Tests.ShouldNotBeNegative;

public class DoubleScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DoubleScenarioShouldFail()
    {
        var @double = -3.5;
        Verify.ShouldFail(() =>
                @double.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"@double
    should not be negative but
-3.5d
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3.5d
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7.5.ShouldNotBeNegative();
    }
}