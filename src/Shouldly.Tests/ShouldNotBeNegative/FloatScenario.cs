namespace Shouldly.Tests.ShouldNotBeNegative;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = -3f;
        Verify.ShouldFail(() =>
                @float.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"@float
    should not be negative but
-3f
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3f
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        7f.ShouldNotBeNegative();
    }
}