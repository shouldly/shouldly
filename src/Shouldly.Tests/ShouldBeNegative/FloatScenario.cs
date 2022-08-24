namespace Shouldly.Tests.ShouldBeNegative;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = 3f;
        Verify.ShouldFail(() =>
                @float.ShouldBeNegative("Some additional context"),

            errorWithSource:
            @"@float
    should be negative but
3f
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3f
    should be negative but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        (-7f).ShouldBeNegative();
    }
}