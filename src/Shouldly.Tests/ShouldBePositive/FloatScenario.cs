namespace Shouldly.Tests.ShouldBePositive;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = -3f;
        Verify.ShouldFail(() =>
                @float.ShouldBePositive("Some additional context"),

            errorWithSource:
            @"@float
    should be positive but
-3f
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3f
    should be positive but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7f.ShouldBePositive();
    }
}