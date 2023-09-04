namespace Shouldly.Tests.ShouldNotBePositive;

[UseCulture("en-US")]
public class DecimalScenario
{
    [Fact]
    public void DecimalScenarioShouldFail()
    {
        var @decimal = 3.5m;
        Verify.ShouldFail(() =>
                @decimal.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"@decimal
    should not be positive but
3.5m
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3.5m
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7.5m).ShouldNotBePositive();
    }
}