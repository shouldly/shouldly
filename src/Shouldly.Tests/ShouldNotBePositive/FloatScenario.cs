namespace Shouldly.Tests.ShouldNotBePositive;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = 3f;
        Verify.ShouldFail(() =>
                @float.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"@float
    should not be positive but
3f
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3f
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7f).ShouldNotBePositive();
    }
}