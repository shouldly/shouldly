namespace Shouldly.Tests.ShouldNotBePositive;

public class LongScenario
{
    [Fact]
    public void LongScenarioShouldFail()
    {
        var val = 3L;
        Verify.ShouldFail(() =>
                val.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"val
    should not be positive but
3L
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3L
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7L).ShouldNotBePositive();
    }
}