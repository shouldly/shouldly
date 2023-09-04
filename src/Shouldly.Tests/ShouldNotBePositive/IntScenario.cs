namespace Shouldly.Tests.ShouldNotBePositive;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = 3;
        Verify.ShouldFail(() =>
                @int.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"@int
    should not be positive but
3
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7).ShouldNotBePositive();
    }
}