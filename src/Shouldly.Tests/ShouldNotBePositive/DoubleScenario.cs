namespace Shouldly.Tests.ShouldNotBePositive;

public class DoubleScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DoubleScenarioShouldFail()
    {
        var @double = 3.5;
        Verify.ShouldFail(() =>
                @double.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"@double
    should not be positive but
3.5d
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3.5d
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7.5).ShouldNotBePositive();
    }
}