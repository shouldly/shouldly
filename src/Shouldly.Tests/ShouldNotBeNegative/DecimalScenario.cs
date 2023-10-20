namespace Shouldly.Tests.ShouldNotBeNegative;

[UseCulture("en-US")]
public class DecimalScenario
{
    [Fact]
    public void DecimalScenarioShouldFail()
    {
        var @decimal = -3.5m;
        Verify.ShouldFail(() =>
                @decimal.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"@decimal
    should not be negative but
-3.5m
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3.5m
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7.5m.ShouldNotBeNegative();
    }
}