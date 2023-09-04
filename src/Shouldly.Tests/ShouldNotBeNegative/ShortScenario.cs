namespace Shouldly.Tests.ShouldNotBeNegative;

public class ShortScenario
{
    [Fact]
    public void ShortScenarioShouldFail()
    {
        var @short = (short)-3;
        Verify.ShouldFail(() =>
                @short.ShouldNotBeNegative("Some additional context"),

            errorWithSource:
            @"@short
    should not be negative but
-3
    is negative

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"-3
    should not be negative but is negative

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        ((short)7).ShouldNotBeNegative();
    }
}